using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject prefab;
    public EnemySettings settings;
    public Vector3 spawnPosition;
    public Transform[] waypoints;
}

public class GameEnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner Settings")]
    public List<EnemySpawnData> enemiesToSpawn = new List<EnemySpawnData>();
    public bool spawnOnStart = true;

    private List<IEnemy> spawnedEnemies = new List<IEnemy>();

    void Start() 
    {
        if (spawnOnStart)
        {
            SpawnAllEnemies();
        }
    }

    public void SpawnAllEnemies()
    {
        EnemySpawner enemySpawner = new DefaultEnemySpawner();
        spawnedEnemies.Clear();

        foreach (var enemyData in enemiesToSpawn)
        {
            if (enemyData.prefab != null && enemyData.settings != null)
            {
                IEnemy enemy = enemySpawner.SpawnEnemy(
                    enemyData.prefab, 
                    enemyData.spawnPosition, 
                    Quaternion.identity, 
                    enemyData.settings,
                    enemyData.waypoints
                );

                spawnedEnemies.Add(enemy);
            }
            else
            {
                Debug.LogWarning("Префаб врага или настройки врага не были назначены!");
            }
        }
    }
}