using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [System.Serializable]
    public class EnemyScorePair
    {
        public EventManager.EnemyType Type;
        public int Score;
    }

    [SerializeField]
    private List<EnemyScorePair> scoreSettings = new()
    {
        new() { Type = EventManager.EnemyType.Regular, Score = 25 },
        new() { Type = EventManager.EnemyType.Fairy, Score = 45 },
        new() { Type = EventManager.EnemyType.Boss, Score = 100 }
    };

    private int score;
    public int сurrentScore => score;
    public event Action<int> OnScoreChanged;

    private void Start()
    {
        EventManager.Instance.OnEnemyKilled += HandleEnemyKilled;
    }

    private void HandleEnemyKilled(EventManager.EnemyType type, int _)
    {
        int pointsToAdd = 10; // значение по умолчанию

        foreach (var setting in scoreSettings)
        {
            Debug.Log(setting.Score);
            if (setting.Type == type)
            {

                pointsToAdd = setting.Score;
                break;
            }
        }

        AddScore(pointsToAdd);
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
}
