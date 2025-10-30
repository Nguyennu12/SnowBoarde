using UnityEngine;
using TMPro; 
public class HUDManager : MonoBehaviour
{
    [Header("Assign Text objects")]
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI speedText;

    GameSession gameSession;
    Rigidbody2D playerRb;

    void Start()
    {
        // Find necessary references
        gameSession = FindFirstObjectByType<GameSession>();
        playerRb = FindFirstObjectByType<PlayerController>().GetComponent<Rigidbody2D>();

        // Update lives at start
        if (gameSession != null)
        {
            livesText.text = "Lives: " + gameSession.playerLives;
        }
    }

    void Update()
    {
        
        if (playerRb != null)
        {
            speedText.text = "Speed: " + playerRb.linearVelocity.magnitude.ToString("F0");
        }
    }

}
