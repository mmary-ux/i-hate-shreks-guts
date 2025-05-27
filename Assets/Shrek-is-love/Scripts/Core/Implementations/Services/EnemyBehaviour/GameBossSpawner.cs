using UnityEngine;

[System.Serializable]
public class BossSpawnData
{
    public GameObject prefab;
    public BossSettings settings;
    public Vector3 spawnPosition;
}

public class GameBossSpawner : MonoBehaviour
{
    [Header("Boss Spawn Settings")]
    public BossSpawnData bossData;
    public bool spawnOnEvent = true;

    private IBoss spawnedBoss;

    private void OnEnable()
    {
        if (spawnOnEvent)
        {
            EventManager.Instance.OnBossSpawn += SpawnBoss;
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnBossSpawn -= SpawnBoss;
        }
    }

    public void SpawnBoss()
    {
        if (spawnedBoss != null)
        {
            if (spawnedBoss is MonoBehaviour bossBehaviour)
            {
                bossBehaviour.gameObject.SetActive(true);
                bossBehaviour.transform.position = bossData.spawnPosition;
            }
        }
        else
        {
            BossSpawner bossSpawner = new DefaultBossSpawner();
            spawnedBoss = bossSpawner.SpawnBoss(
                bossData.prefab,
                bossData.spawnPosition,
                Quaternion.identity,
                bossData.settings
            );
        }
    }
}