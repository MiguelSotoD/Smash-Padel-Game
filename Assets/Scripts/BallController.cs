using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Configuración de Física")]
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float bounceForce = 1f;
    [SerializeField] private float minBounceVelocity = 3f;
    
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    
    [Header("Configuración de Rebote")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundBounceForce = 8f;
    [SerializeField] private float ceilingBounceForce = 8f;
    
    private Vector2 initialDirection;
    private bool hasStarted = false;
    private GameManager gameManager;
    private Camera mainCamera;
    private float groundY;
    private float ceilingY;
    private float leftBound;
    private float rightBound;
    public PlayerSoundController playerSoundController;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.gravityScale = 0f; // Sin gravedad, la pelota se mueve libremente
            rb.linearDamping = 0f; // Sin fricción
        }
        
        gameManager = FindObjectOfType<GameManager>();
        mainCamera = Camera.main;
        
        // Calcula los límites de la pantalla
        CalculateBounds();
    }
    
    private void CalculateBounds()
    {
        if (mainCamera != null && mainCamera.orthographic)
        {
            float orthoHeight = mainCamera.orthographicSize;
            float orthoWidth = orthoHeight * mainCamera.aspect;
            Vector3 camPos = mainCamera.transform.position;
            
            groundY = camPos.y - orthoHeight + 0.5f; // Margen inferior
            ceilingY = camPos.y + orthoHeight - 0.5f; // Margen superior
            leftBound = camPos.x - orthoWidth;
            rightBound = camPos.x + orthoWidth;
        }
        else
        {
            // Valores por defecto si no hay cámara
            groundY = -4f;
            ceilingY = 4f;
            leftBound = -8f;
            rightBound = 8f;
        }
    }
    
    private void Start()
    {
        // Inicia el movimiento después de un breve delay
        Invoke(nameof(StartBall), 1f);
    }
    
    private void StartBall()
    {
        // Dirección aleatoria inicial
        float randomDirection = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
        initialDirection = new Vector2(randomDirection, Random.Range(-0.3f, 0.3f)).normalized;
        rb.linearVelocity = initialDirection * initialSpeed;
        hasStarted = true;
    }
    
    private void Update()
    {
        // Limita la velocidad máxima
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        
        // Asegura velocidad mínima
        if (hasStarted && rb.linearVelocity.magnitude < minBounceVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * minBounceVelocity;
        }
        
        // Limita la posición de la pelota para evitar que se salga de los límites
        CheckBounds();
    }
    
    private void CheckBounds()
    {
        Vector3 pos = transform.position;
        
        // Límite superior (techo) - solo como respaldo si no hay collider
        if (pos.y > ceilingY)
        {
            pos.y = ceilingY;
            // Rebota en el techo solo si está subiendo
            if (rb.linearVelocity.y > 0)
            {
                Vector2 newVel = rb.linearVelocity;
                newVel.y = -Mathf.Abs(newVel.y) * 0.7f; // Rebote con pérdida mínima
                // Limita la velocidad para evitar bugs
                if (newVel.magnitude > maxSpeed)
                {
                    newVel = newVel.normalized * maxSpeed;
                }
                rb.linearVelocity = newVel;
            }
            transform.position = pos;
        }
        
        // Límite inferior (suelo) - solo como respaldo si no hay collider
        // Normalmente el collider del suelo maneja esto
        if (pos.y < groundY)
        {
            pos.y = groundY;
            // Rebota en el suelo solo si está cayendo
            if (rb.linearVelocity.y < 0)
            {
                Vector2 newVel = rb.linearVelocity;
                newVel.y = Mathf.Max(Mathf.Abs(newVel.y) * 0.7f, minBounceVelocity);
                // Limita la velocidad para evitar bugs
                if (newVel.magnitude > maxSpeed)
                {
                    newVel = newVel.normalized * maxSpeed;
                }
                rb.linearVelocity = newVel;
            }
            transform.position = pos;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Rebote en el suelo - siempre rebota automáticamente
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Si la pelota está cayendo, rebota hacia arriba
            if (rb.linearVelocity.y <= 0)
            {
                // Mantiene la velocidad horizontal
                float horizontalSpeed = rb.linearVelocity.x;
                
                // Calcula la velocidad de rebote basada en la velocidad actual
                // pero asegura una velocidad mínima
                float currentSpeed = Mathf.Max(rb.linearVelocity.magnitude, minBounceVelocity);
                float bounceSpeed = Mathf.Max(groundBounceForce * 0.5f, minBounceVelocity);
                
                // Limita la velocidad de rebote para evitar acumulación
                bounceSpeed = Mathf.Min(bounceSpeed, maxSpeed * 0.8f);
                
                // Asegura que siempre rebote hacia arriba con velocidad controlada
                Vector2 bounceDirection = new Vector2(
                    horizontalSpeed,
                    bounceSpeed
                ).normalized;
                
                // Aplica el rebote manteniendo la velocidad horizontal
                rb.linearVelocity = new Vector2(horizontalSpeed, bounceSpeed);
                
                // Asegura velocidad mínima total
                if (rb.linearVelocity.magnitude < minBounceVelocity)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * minBounceVelocity;
                }
            }
        }
        
        // Rebote en el techo
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            // Si la pelota está subiendo, rebota hacia abajo
            if (rb.linearVelocity.y > 0)
            {
                float horizontalSpeed = rb.linearVelocity.x;
                float bounceSpeed = Mathf.Abs(rb.linearVelocity.y) * 0.8f; // Rebote con pérdida mínima
                bounceSpeed = Mathf.Max(bounceSpeed, minBounceVelocity * 0.5f);
                
                rb.linearVelocity = new Vector2(horizontalSpeed, -bounceSpeed);
                
                // Limita la velocidad máxima
                if (rb.linearVelocity.magnitude > maxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                }
            }
        }
        
        // Rebote en los bordes laterales (paredes verticales)
        if (collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint2D contact = collision.contacts[0];
            // Solo rebota horizontalmente si es una pared vertical
            if (Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y))
            {
                Vector2 newVelocity = rb.linearVelocity;
                newVelocity.x = -newVelocity.x;
                rb.linearVelocity = newVelocity;
            }
            // Si es una pared horizontal (techo), rebota verticalmente
            else if (contact.normal.y < -0.5f && rb.linearVelocity.y > 0)
            {
                float horizontalSpeed = rb.linearVelocity.x;
                float bounceSpeed = Mathf.Abs(rb.linearVelocity.y) * 0.8f;
                bounceSpeed = Mathf.Max(bounceSpeed, minBounceVelocity * 0.5f);
                
                rb.linearVelocity = new Vector2(horizontalSpeed, -bounceSpeed);
                
                if (rb.linearVelocity.magnitude > maxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta cuando la pelota sale por los lados (punto)
        if (other.CompareTag("LeftGoal"))
        {
            // Jugador 2 anota un punto
            if (gameManager != null)
            {
                gameManager.PlayerScores(2);
                playerSoundController.PlayPunto();
            }
            ResetBall();
        }
        else if (other.CompareTag("RightGoal"))
        {
            // Jugador 1 anota un punto
            if (gameManager != null)
            {
                gameManager.PlayerScores(1);
                playerSoundController.PlayPunto();
            }
            ResetBall();
        }
    }
    
    public void ResetBall()
    {
        hasStarted = false;
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        Invoke(nameof(StartBall), 1f);
    }
    
    public void HitBall(Vector2 direction, float power)
    {
        // Aplica un golpe a la pelota
        rb.linearVelocity = direction.normalized * power;
        playerSoundController.PlayRaqueta();
    }
    
    public void SetSpeed(float speed)
    {
        initialSpeed = speed;
        if (hasStarted)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }
    
    public Vector2 GetVelocity()
    {
        return rb.linearVelocity;
    }
}

