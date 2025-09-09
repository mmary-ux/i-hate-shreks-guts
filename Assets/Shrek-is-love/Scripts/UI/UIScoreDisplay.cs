using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreSystem scoreSystem;

    void Start()
    {
        scoreSystem.OnScoreChanged += UpdateScoreDisplay;
        UpdateScoreDisplay(scoreSystem.ñurrentScore);
    }
    private void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}
