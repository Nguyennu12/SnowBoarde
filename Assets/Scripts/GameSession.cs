using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int startingLives = 3;
    public int playerLives;

    // Stores the last played level index
    private int lastLevelIndex = 1;

    void Awake()
    {
        // --- Singleton logic ---
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            playerLives = startingLives; // Initialize lives only once
        }
    }

    // Called when the player dies
    public void ProcessPlayerDeath()
    {
        playerLives--;
        lastLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (playerLives <= 0)
        {
            SceneManager.LoadScene("GameOver"); // No lives left
        }
        else
        {
            SceneManager.LoadScene(lastLevelIndex); // Retry current level
        }
    }

    // Called by "Restart" button
    public void RestartFromLastLevel()
    {
        playerLives = startingLives;
        SceneManager.LoadScene(lastLevelIndex);
    }

    // Called by "Main Menu" button
    public void ReturnToMainMenu()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
