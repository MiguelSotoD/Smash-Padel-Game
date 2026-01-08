using UnityEngine;

public class RacketController : MonoBehaviour
{
    [Header("Configuración de Golpe")]
    [SerializeField] private float hitForce = 12f;
    [SerializeField] private float sweetSpotForce = 18f;
    [SerializeField] private float sweetSpotRadius = 0.3f;
    
    [Header("Referencias")]
    [SerializeField] private Transform sweetSpotCenter;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BallController ball;
    
    [Header("Efectos Visuales")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject sweetSpotEffect;
    
    private int playerNumber;
    private bool canHit = true;
    private float hitCooldown = 0.2f;
    private float lastHitTime = 0f;
    
    private void Awake()
    {
        if (playerController != null)
        {
            playerNumber = playerController.GetPlayerNumber();
        }
        
        // Si no hay sweetSpotCenter asignado, usa el centro de la raqueta
        if (sweetSpotCenter == null)
        {
            sweetSpotCenter = transform;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            HitBall(other.gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HitBall(collision.gameObject);
        }
    }
    
    private void HitBall(GameObject ballObject)
    {
        if (!canHit || Time.time - lastHitTime < hitCooldown)
            return;
        
        BallController ballController = ballObject.GetComponent<BallController>();
        if (ballController == null)
            return;
        
        Vector2 ballPosition = ballObject.transform.position;
        Vector2 racketPosition = transform.position;
        Vector2 currentVelocity = ballController.GetVelocity();
        
        // Verifica si es un golpe en el sweet spot
        float distanceToSweetSpot = Vector2.Distance(ballPosition, sweetSpotCenter.position);
        bool isSweetSpot = distanceToSweetSpot <= sweetSpotRadius;
        
        // Calcula la fuerza del golpe
        float force = isSweetSpot ? sweetSpotForce : hitForce;
        
        // Calcula la dirección del golpe
        // Si la pelota viene hacia la raqueta, la devuelve en dirección opuesta
        // Si no, usa la dirección desde la raqueta hacia la pelota
        Vector2 hitDirection;
        
        // Determina hacia qué lado está el jugador (izquierda o derecha)
        int playerSide = playerNumber == 1 ? -1 : 1; // Jugador 1 izquierda, Jugador 2 derecha
        
        // Si la pelota viene hacia el jugador, la devuelve
        if ((playerSide < 0 && currentVelocity.x < 0) || (playerSide > 0 && currentVelocity.x > 0))
        {
            // La pelota viene hacia el jugador, devuélvela
            hitDirection = new Vector2(-playerSide, Random.Range(-0.2f, 0.5f)).normalized;
        }
        else
        {
            // La pelota se aleja, golpéala hacia el oponente
            hitDirection = new Vector2(playerSide, Random.Range(-0.2f, 0.5f)).normalized;
        }
        
        // Ajusta la dirección basada en la posición relativa de la pelota
        float verticalOffset = (ballPosition.y - racketPosition.y) * 0.5f;
        hitDirection = new Vector2(hitDirection.x, hitDirection.y + verticalOffset).normalized;
        
        // Aplica el golpe
        ballController.HitBall(hitDirection, force);
        
        // Efectos visuales
        if (isSweetSpot && sweetSpotEffect != null)
        {
            Instantiate(sweetSpotEffect, ballPosition, Quaternion.identity);
        }
        else if (hitEffect != null)
        {
            Instantiate(hitEffect, ballPosition, Quaternion.identity);
        }
        
        lastHitTime = Time.time;
        
        // Reproduce sonido (se puede agregar AudioSource después)
        PlayHitSound(isSweetSpot);
    }
    
    private void PlayHitSound(bool isSweetSpot)
    {
        // TODO: Agregar AudioSource y sonidos
        // if (audioSource != null)
        // {
        //     audioSource.PlayOneShot(isSweetSpot ? sweetSpotSound : normalHitSound);
        // }
    }
    
    private void OnDrawGizmosSelected()
    {
        // Dibuja el área del sweet spot en el editor
        if (sweetSpotCenter != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(sweetSpotCenter.position, sweetSpotRadius);
        }
    }
    
    public void SetHitForce(float force)
    {
        hitForce = force;
    }
    
    public void SetSweetSpotForce(float force)
    {
        sweetSpotForce = force;
    }
}

