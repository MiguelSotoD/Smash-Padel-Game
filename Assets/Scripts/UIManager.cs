using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameModeText;
    
    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        
        UpdateScore(0, 0);
    }
    
    public void UpdateScore(int player1Score, int player2Score)
    {
        if (player1ScoreText != null)
            player1ScoreText.text = player1Score.ToString();
        
        if (player2ScoreText != null)
            player2ScoreText.text = player2Score.ToString();
        
        // Animación de punto (opcional)
        AnimateScore();
    }
    
    public void ShowGameOver(int winner)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (gameOverText != null)
        {
            gameOverText.text = $"¡Jugador {winner} Gana!";
        }
    }
    
    public void UpdateGameMode(GameMode mode)
    {
        if (gameModeText != null)
        {
            string modeName = mode switch
            {
                GameMode.Classic => "Clásico",
                GameMode.FastBall => "Pelota Rápida",
                GameMode.LowGravity => "Gravedad Baja",
                GameMode.Random => "Aleatorio",
                _ => "Desconocido"
            };
            gameModeText.text = $"Modo: {modeName}";
        }
    }
    
    private void AnimateScore()
    {
        // TODO: Agregar animación de punto
        // Se puede usar DOTween o animaciones de Unity
    }
    
    public void HideGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
}

