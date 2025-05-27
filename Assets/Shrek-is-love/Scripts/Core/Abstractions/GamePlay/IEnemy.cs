using UnityEngine;

public interface IEnemy
{
    EnemyAI GetEnemyAI();
    void Initialize(EnemySettings settings, Transform[] waypoints);
}