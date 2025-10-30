using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 1.5f; 
    [SerializeField] float duration = 5f; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Apply speed boost
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySpeedBoost(speedMultiplier, duration);
            }

            // Disable pickup and remove later
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, duration + 1f);
        }
    }
}
