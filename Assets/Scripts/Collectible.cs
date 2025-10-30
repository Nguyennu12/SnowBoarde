using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int points = 10; // Points this collectible gives
    [SerializeField] ScoreManager scoreManager; 

    void Start()
    {
       
        if (scoreManager == null)
        {
            scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        }
    }

    // Called when something passes through the Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            
            if (scoreManager != null)
            {
                scoreManager.AddScore(points);
            }

           
            Destroy(gameObject);
        }
    }
}
