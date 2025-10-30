using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI scoreText;

    
    int currentScore = 0;

    void Start()
    {
        
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;
    }

    
    public void AddScore(int pointsToAdd)
    {
        currentScore = currentScore + pointsToAdd;
       
        scoreText.text = "Score: " + currentScore;
    }
}