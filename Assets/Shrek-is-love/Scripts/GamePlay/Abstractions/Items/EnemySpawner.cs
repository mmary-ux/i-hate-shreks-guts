using UnityEngine;

public abstract class EnemySpawner
{
    public abstract IEnemy SpawnEnemy(GameObject prefab, Vector3 position, Quaternion rotation, 
                                    EnemySettings settings, Transform[] waypoints = null);
}
