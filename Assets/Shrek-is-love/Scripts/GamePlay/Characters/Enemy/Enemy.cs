using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private EnemyAI enemyAI;

    public EnemyAI GetEnemyAI() => enemyAI;

    public void Initialize(EnemySettings settings, Transform[] waypoints)
    {
        enemyAI = GetComponent<EnemyAI>();
        if (enemyAI == null)
        {
            enemyAI = gameObject.AddComponent<EnemyAI>();
        }
        
        enemyAI.settings = settings;
        enemyAI.waypoints = waypoints;
    }
}