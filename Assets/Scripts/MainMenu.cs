using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called by the "Start" button
    public void HandleStartButton()
    {
        SceneManager.LoadScene("Level1"); // Load the first level
    }

   

    // Called by the "Quit" button
    public void HandleQuitButton()
    {
        Debug.Log("Game exited!");
        Application.Quit(); // Works only in a built game
    }
}
