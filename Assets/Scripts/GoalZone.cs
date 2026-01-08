using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] private int playerNumber; // 1 = izquierda (punto para jugador 2), 2 = derecha (punto para jugador 1)
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                // Si es la zona izquierda, el jugador 2 anota
                // Si es la zona derecha, el jugador 1 anota
                int scoringPlayer = playerNumber == 1 ? 2 : 1;
                gameManager.PlayerScores(scoringPlayer);
            }
        }
    }
}

