using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public enum GameMode
{
    Classic,        // Partida hasta X puntos
    FastBall,       // Pelota rápida
    LowGravity,     // Gravedad baja
    Random          // Modo aleatorio cada ronda
}

public class GameManager : MonoBehaviour
{
    [Header("Configuración del Juego")]
    [SerializeField] private int pointsToWin = 5;
    [SerializeField] private GameMode currentGameMode = GameMode.Classic;
    
    [Header("Referencias")]
    [SerializeField] private BallController ball;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    [SerializeField] private Text player1ScoreTextLegacy; // Para Text (Legacy)
    [SerializeField] private Text player2ScoreTextLegacy; // Para Text (Legacy)

    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;
    [SerializeField] private Transform ballSpawnPoint;
    
    [Header("Configuración de Modos")]
    [SerializeField] private float fastBallSpeed = 8f;
    [SerializeField] private float lowGravityScale = 1f;
    
    private int player1Score = 0;
    private int player2Score = 0;
    private bool gameOver = false;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gm = new GameObject("GameManager");
                    instance = gm.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    
    public void paddle1Scored()
    {
        player1Score++;
        UpdatePlayer1ScoreText();
        UpdateUI();
        CheckGameOver();
    }

    public void paddle2Scored()
    {
        player2Score++;
        UpdatePlayer2ScoreText();
        UpdateUI();
        CheckGameOver();
    }
    
    // Método auxiliar para actualizar el texto del jugador 1
    private void UpdatePlayer1ScoreText()
    {
        string scoreText = player1Score.ToString();
        if (player1ScoreText != null)
        {
            player1ScoreText.text = scoreText;
        }
        if (player1ScoreTextLegacy != null)
        {
            player1ScoreTextLegacy.text = scoreText;
        }
    }
    
    // Método auxiliar para actualizar el texto del jugador 2
    private void UpdatePlayer2ScoreText()
    {
        string scoreText = player2Score.ToString();
        if (player2ScoreText != null)
        {
            player2ScoreText.text = scoreText;
        }
        if (player2ScoreTextLegacy != null)
        {
            player2ScoreTextLegacy.text = scoreText;
        }
    }

    public void Restart()
    {
        // Reinicia las posiciones de los jugadores y la pelota
        if (player1 != null && player1SpawnPoint != null)
        {
            player1.transform.position = player1SpawnPoint.position;
        }
        if (player2 != null && player2SpawnPoint != null)
        {
            player2.transform.position = player2SpawnPoint.position;
        }
        if (ball != null && ballSpawnPoint != null)
        {
            ball.transform.position = ballSpawnPoint.position;
            ball.ResetBall();
        }
    }

    private void Start()
    {
        if (ball == null)
            ball = FindObjectOfType<BallController>();
        
        if (player1 == null || player2 == null)
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            foreach (var player in players)
            {
                if (player.GetPlayerNumber() == 1)
                    player1 = player;
                else if (player.GetPlayerNumber() == 2)
                    player2 = player;
            }
        }
        
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
        
        // Buscar los textos si no están asignados (busca TextMeshPro y Text Legacy)
        if (player1ScoreText == null && player1ScoreTextLegacy == null)
        {
            // Buscar por nombre en toda la escena
            GameObject textObj = GameObject.Find("Paddel1Score");
            if (textObj == null)
                textObj = GameObject.Find("Paddle1Score"); // Por si acaso está escrito diferente
            
            if (textObj != null)
            {
                // Intentar primero con TextMeshPro
                player1ScoreText = textObj.GetComponent<TextMeshProUGUI>();
                if (player1ScoreText == null)
                {
                    // Si no tiene TextMeshPro, intentar con Text (Legacy)
                    player1ScoreTextLegacy = textObj.GetComponent<Text>();
                    if (player1ScoreTextLegacy != null)
                    {
                        Debug.Log("✓ Paddel1Score encontrado (Text Legacy) y asignado correctamente");
                    }
                    else
                    {
                        Debug.LogError("Paddel1Score encontrado pero no tiene componente TextMeshProUGUI ni Text. " +
                            "Asegúrate de que tiene uno de estos componentes.");
                    }
                }
                else
                {
                    Debug.Log("✓ Paddel1Score encontrado (TextMeshPro) y asignado correctamente");
                }
            }
            else
            {
                Debug.LogError("No se encontró el objeto Paddel1Score. " +
                    "Asegúrate de que existe en la escena con ese nombre exacto.");
            }
        }
        else
        {
            Debug.Log("✓ Paddel1Score ya estaba asignado en el Inspector");
        }
        
        if (player2ScoreText == null && player2ScoreTextLegacy == null)
        {
            // Buscar por nombre en toda la escena
            GameObject textObj = GameObject.Find("Paddel2Score");
            if (textObj == null)
                textObj = GameObject.Find("Paddle2Score"); // Por si acaso está escrito diferente
            
            if (textObj != null)
            {
                // Intentar primero con TextMeshPro
                player2ScoreText = textObj.GetComponent<TextMeshProUGUI>();
                if (player2ScoreText == null)
                {
                    // Si no tiene TextMeshPro, intentar con Text (Legacy)
                    player2ScoreTextLegacy = textObj.GetComponent<Text>();
                    if (player2ScoreTextLegacy != null)
                    {
                        Debug.Log("✓ Paddel2Score encontrado (Text Legacy) y asignado correctamente");
                    }
                    else
                    {
                        Debug.LogError("Paddel2Score encontrado pero no tiene componente TextMeshProUGUI ni Text. " +
                            "Asegúrate de que tiene uno de estos componentes.");
                    }
                }
                else
                {
                    Debug.Log("✓ Paddel2Score encontrado (TextMeshPro) y asignado correctamente");
                }
            }
            else
            {
                Debug.LogError("No se encontró el objeto Paddel2Score. " +
                    "Asegúrate de que existe en la escena con ese nombre exacto.");
            }
        }
        else
        {
            Debug.Log("✓ Paddel2Score ya estaba asignado en el Inspector");
        }
        
        // Inicializar los textos a "0"
        player1Score = 0;
        player2Score = 0;
        UpdatePlayer1ScoreText();
        UpdatePlayer2ScoreText();
        
        ApplyGameMode();
        UpdateUI();
    }
    
    public void PlayerScores(int playerNumber)
    {
        if (gameOver)
            return;
        
        if (playerNumber == 1)
        {
            player1Score++;
        }
        else if (playerNumber == 2)
        {
            player2Score++;
        }
        
        UpdateUI();
        CheckGameOver();
        
        // Si no hay game over, cambia el modo si es aleatorio
        if (!gameOver && currentGameMode == GameMode.Random)
        {
            StartCoroutine(ChangeRandomModeAfterDelay(1f));
        }
    }
    
    private void CheckGameOver()
    {
        if (player1Score >= pointsToWin)
        {
            gameOver = true;
            EndGame(1);
        }
        else if (player2Score >= pointsToWin)
        {
            gameOver = true;
            EndGame(2);
        }
    }
    
    private void EndGame(int winner)
    {
        if (uiManager != null)
        {
            uiManager.ShowGameOver(winner);
        }
        
        // Reinicia el juego después de 3 segundos
        Invoke(nameof(RestartGame), 3f);
    }
    
    private void RestartGame()
    {
        player1Score = 0;
        player2Score = 0;
        gameOver = false;
        UpdateUI();
        
        // Reiniciar posiciones
        Restart();
    }
    
    private void ApplyGameMode()
    {
        switch (currentGameMode)
        {
            case GameMode.Classic:
                // Configuración normal
                if (ball != null)
                    ball.SetSpeed(5f);
                if (player1 != null)
                    player1.SetGravityScale(2f);
                if (player2 != null)
                    player2.SetGravityScale(2f);
                break;
                
            case GameMode.FastBall:
                // Pelota más rápida
                if (ball != null)
                    ball.SetSpeed(fastBallSpeed);
                break;
                
            case GameMode.LowGravity:
                // Gravedad baja (saltos más flotantes)
                if (player1 != null)
                    player1.SetGravityScale(lowGravityScale);
                if (player2 != null)
                    player2.SetGravityScale(lowGravityScale);
                break;
                
            case GameMode.Random:
                // Aplica un modo aleatorio
                ApplyRandomMode();
                break;
        }
    }
    
    private void ApplyRandomMode()
    {
        GameMode randomMode = (GameMode)Random.Range(0, 3); // 0-2 (excluye Random)
        
        switch (randomMode)
        {
            case GameMode.Classic:
                if (ball != null)
                    ball.SetSpeed(5f);
                if (player1 != null)
                    player1.SetGravityScale(2f);
                if (player2 != null)
                    player2.SetGravityScale(2f);
                break;
                
            case GameMode.FastBall:
                if (ball != null)
                    ball.SetSpeed(fastBallSpeed);
                break;
                
            case GameMode.LowGravity:
                if (player1 != null)
                    player1.SetGravityScale(lowGravityScale);
                if (player2 != null)
                    player2.SetGravityScale(lowGravityScale);
                break;
        }
    }
    
    private IEnumerator ChangeRandomModeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ApplyRandomMode();
    }
    
    private void UpdateUI()
    {
        // Actualizar los textos del canvas (funciona con TextMeshPro y Text Legacy)
        UpdatePlayer1ScoreText();
        UpdatePlayer2ScoreText();
        
        // Actualizar el UIManager si existe
        if (uiManager != null)
        {
            uiManager.UpdateScore(player1Score, player2Score);
        }
    }
    
    public void SetGameMode(GameMode mode)
    {
        currentGameMode = mode;
        ApplyGameMode();
    }
    
    public int GetPlayer1Score()
    {
        return player1Score;
    }
    
    public int GetPlayer2Score()
    {
        return player2Score;
    }
    
    public GameMode GetCurrentGameMode()
    {
        return currentGameMode;
    }
}

