using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Salto")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxHeight = 4f;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private float jumpCooldown = 0.1f;
    
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Configuración del Jugador")]
    [SerializeField] private int playerNumber = 1; // 1 o 2
    
    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float groundCheckDistance = 0.15f;
    
    // Estados internos
    private bool isJumping = false;
    private bool canJump = true;
    private float lastJumpTime = 0f;
    private float groundY;
    private float topY;
    private Camera mainCamera;
    
    // Input
    private bool jumpInput = false;
    private bool jumpInputHeld = false;
    
    // Nombres de los parámetros del Animator (DEBEN coincidir exactamente con el Animator Controller)
    private const string PARAM_IS_GROUNDED = "IsGrounded";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string PARAM_IS_FALLING = "IsFalling";

    public PlayerSoundController playerSoundController;
    
    private void Awake()
    {
        // Obtiene o busca el Rigidbody2D
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.gravityScale = gravityScale;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        
        // Obtiene o busca el Animator
        if (animator == null)
            animator = GetComponent<Animator>();
        
        // Configuración inicial
        mainCamera = Camera.main;
        groundY = transform.position.y;
        CalculateTopLimit();
    }
    
    private void CalculateTopLimit()
    {
        if (mainCamera != null && mainCamera.orthographic)
        {
            float cameraTop = mainCamera.transform.position.y + mainCamera.orthographicSize;
            topY = cameraTop - 0.3f;
        }
        else
        {
            topY = groundY + maxHeight;
        }
        
        if (topY < groundY + 3f)
        {
            topY = groundY + maxHeight;
        }
    }
    
    private void Update()
    {
        HandleInput();
        CheckMaxHeight();
        UpdateAnimator();
    }
    
    private void FixedUpdate()
    {
        HandleJump();
    }
    
    private void HandleInput()
    {
        // Jugador 1: W o Espacio
        if (playerNumber == 1)
        {
            jumpInput = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
            jumpInputHeld = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
        }
        // Jugador 2: Flecha Arriba
        else if (playerNumber == 2)
        {
            jumpInput = Input.GetKeyDown(KeyCode.UpArrow);
            jumpInputHeld = Input.GetKey(KeyCode.UpArrow);
        }
    }
    
    private void HandleJump()
    {
        // Saltos consecutivos mientras mantiene presionado
        if (jumpInputHeld && Time.time - lastJumpTime >= jumpCooldown)
        {
            if (transform.position.y < topY)
            {
                if (rb.linearVelocity.y < jumpForce * 0.3f)
                {
                    rb.linearVelocity = new Vector2(0, Mathf.Min(jumpForce * 0.8f, (topY - transform.position.y) * 2f));
                    lastJumpTime = Time.time;
                    playerSoundController.PlaySaltar();
                }
            }
        }
        
        // Salto inicial
        if (jumpInput && canJump && Time.time - lastJumpTime >= jumpCooldown)
        {
            if (transform.position.y < topY)
            {
                float forceToApply = jumpForce;
                float distanceToTop = topY - transform.position.y;
                if (distanceToTop < 1f)
                {
                    forceToApply = Mathf.Min(forceToApply, distanceToTop * 3f);
                }
                rb.linearVelocity = new Vector2(0, forceToApply);
                lastJumpTime = Time.time;
                isJumping = true;
            }
        }
    }
    
    private void CheckMaxHeight()
    {
        // Limita la altura máxima (techo)
        if (transform.position.y > topY)
        {
            transform.position = new Vector3(transform.position.x, topY, transform.position.z);
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
        }
        
        // Evita que caiga por debajo del suelo
        if (transform.position.y < groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                isJumping = false;
                canJump = true;
            }
        }
    }
    
    private void UpdateAnimator()
    {
        if (animator == null) return;
        
        // Detecta si está en el suelo
        bool grounded = IsGrounded();
        
        // Obtiene la velocidad vertical
        float velocityY = rb != null ? rb.linearVelocity.y : 0f;
        
        // Determina los estados de animación
        bool isGroundedState = grounded && Mathf.Abs(velocityY) < 0.2f;
        bool isJumpingState = !grounded && velocityY > 0.3f;
        bool isFallingState = !grounded && velocityY < -0.3f;
        
        // Actualiza los parámetros del Animator
        animator.SetBool(PARAM_IS_GROUNDED, isGroundedState);
        animator.SetBool(PARAM_IS_JUMPING, isJumpingState);
        animator.SetBool(PARAM_IS_FALLING, isFallingState);
    }
    
    private bool IsGrounded()
    {
        // Método 1: Usar groundCheck con OverlapCircle
        if (groundCheck != null)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (hit != null)
            {
                return true;
            }
        }
        
        // Método 2: Raycast hacia abajo
        if (rb != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                groundCheckDistance,
                groundLayer
            );
            
            if (hit.collider != null)
            {
                return true;
            }
        }
        
        // Método 3: Verificar colisión con tag Ground
        Collider2D groundCollider = Physics2D.OverlapCircle(
            new Vector2(transform.position.x, transform.position.y - groundCheckDistance),
            groundCheckRadius
        );
        
        if (groundCollider != null && groundCollider.CompareTag("Ground"))
        {
            return true;
        }
        
        // Método 4: Fallback - verificar posición Y
        bool nearGround = Mathf.Abs(transform.position.y - groundY) < groundCheckDistance;
        bool notMovingUp = rb == null || rb.linearVelocity.y <= 0.1f;
        
        return nearGround && notMovingUp;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            canJump = true;
        }
        
        // Detecta cuando la raqueta choca con la pelota
        if (collision.gameObject.CompareTag("Ball"))
        {
            playerSoundController.PlayRaqueta();
        }
    }
    
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    
    public bool IsJumping()
    {
        return isJumping || (rb != null && rb.linearVelocity.y > 0.1f);
    }
    
    public void SetMaxHeight(float height)
    {
        maxHeight = height;
        CalculateTopLimit();
    }
    
    public void SetGravityScale(float gravity)
    {
        gravityScale = gravity;
        if (rb != null)
            rb.gravityScale = gravity;
    }
    
    // Método para debug - visualizar el groundCheck en el editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z),
                groundCheckRadius
            );
        }
    }
}
