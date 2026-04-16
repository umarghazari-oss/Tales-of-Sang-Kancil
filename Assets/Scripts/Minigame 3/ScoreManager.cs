using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int scoreCount;
    
    void Awake()
    {
        scoreCount = 0;
        scoreText.text = "Score: " + scoreCount;
    }

    public void UpdateScore(int scoreNum)
    {
        scoreCount += scoreNum;
        if (scoreCount < 0)
        {
            scoreCount = 0;
        }
        scoreText.text = "Score: " + scoreCount;
    }
}
