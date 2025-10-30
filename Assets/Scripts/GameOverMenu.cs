using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    GameSession session;

    void Start()
    {
        // Find the persistent GameSession
        session = FindFirstObjectByType<GameSession>();
    }

    // Called by the "Restart" button
    public void HandleRestartButton()
    {
        if (session != null)
        {
            session.RestartFromLastLevel(); // Restart the last played level
        }
        else
        {
            SceneManager.LoadScene("MainMenu"); // Fallback
        }
    }

    // Called by the "Main Menu" button
    public void HandleMenuButton()
    {
        if (session != null)
        {
            session.ReturnToMainMenu(); // Return to main menu
        }
        else
        {
            SceneManager.LoadScene("MainMenu"); // Fallback
        }
    }
}
