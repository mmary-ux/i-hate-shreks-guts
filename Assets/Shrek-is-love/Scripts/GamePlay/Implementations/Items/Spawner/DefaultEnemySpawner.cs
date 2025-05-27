using UnityEngine;

public class DefaultEnemySpawner : EnemySpawner
{
    public override IEnemy SpawnEnemy(GameObject prefab, Vector3 position, Quaternion rotation, 
                                     EnemySettings settings, Transform[] waypoints = null)
    {   
        position.y = 0;

        GameObject enemyObj = Object.Instantiate(prefab, position, rotation);

        var agent = enemyObj.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent == null)
        {
            agent = enemyObj.AddComponent<UnityEngine.AI.NavMeshAgent>();
        }

        agent.Warp(position);

        IEnemy enemy = enemyObj.GetComponent<IEnemy>();
        if (enemy == null)
        {
            enemy = enemyObj.AddComponent<Enemy>();
        }
        
        enemy.Initialize(settings, waypoints);
        return enemy;
    }
}