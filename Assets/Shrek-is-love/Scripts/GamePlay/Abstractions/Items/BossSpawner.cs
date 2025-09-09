using UnityEngine;

public abstract class BossSpawner
{
    public abstract IBoss SpawnBoss(GameObject prefab, Vector3 position, Quaternion rotation, BossSettings settings);
}