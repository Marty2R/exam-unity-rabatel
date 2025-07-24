using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager Instance;
    
    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    
    [Header("Game Settings")]
    public float gameTime = 45f;
    public int totalCollectables = 5;
    
    [Header("Scene Names")]
    public string menuSceneName = "MenuScene";
    public string gameSceneName = "GameScene";
    
    private float currentTime;
    private int collectedItems = 0;
    private bool gameEnded = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        currentTime = gameTime;
        collectedItems = 0;
        gameEnded = false;
        
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
        if (collectables.Length > 0)
        {
            totalCollectables = collectables.Length;
        }
        
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        
        UpdateTimerUI();
        
        Debug.Log($"Jeu démarré ! {totalCollectables} objets à collecter en {gameTime} secondes.");
    }
    
    void Update()
    {
        if (!gameEnded)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
            
            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }
    
    public void CollectItem()
    {
        if (gameEnded) return;
        
        collectedItems++;
        Debug.Log($"Objet collecté ! {collectedItems}/{totalCollectables}");
        
        if (collectedItems >= totalCollectables)
        {
            Victory();
        }
    }
    
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
                        int seconds = (int)Mathf.Ceil(currentTime);

            timerText.text = "Temps: " + seconds.ToString();
            
            if (seconds <= 10)
            {
                timerText.color = Color.red;
            }
            else if (seconds <= 20)
            {
                timerText.color = Color.yellow;
            }
            else
            {
                timerText.color = Color.white;
            }
        }
    }
    
    void GameOver()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Debug.Log("Game Over ! Temps écoulé.");
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
  
    }
    
    void Victory()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Debug.Log("Victoire ! Tous les objets collectés !");
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        
    }
    
    public void RestartGame()
    {
        Debug.Log("Redémarrage du jeu...");
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(gameSceneName);
    }
    
    public void ReturnToMenu()
    {
        Debug.Log("Retour au menu principal...");
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(menuSceneName);
    }
    
    public int GetCollectedItems()
    {
        return collectedItems;
    }
    
    public int GetTotalCollectables()
    {
        return totalCollectables;
    }
    
    public float GetTimeRemaining()
    {
        return currentTime;
    }
    
    public bool IsGameEnded()
    {
        return gameEnded;
    }
    
    // Optionnel : Méthode pour forcer la victoire (pour tester)
    [ContextMenu("Force Victory")]
    public void ForceVictory()
    {
        Victory();
    }
    
    // Optionnel : Méthode pour forcer le game over (pour tester)
    [ContextMenu("Force Game Over")]
    public void ForceGameOver()
    {
        GameOver();
    }
}