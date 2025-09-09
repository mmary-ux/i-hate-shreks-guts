using UnityEngine;

public class DefaultBossSpawner : BossSpawner
{
    public override IBoss SpawnBoss(GameObject prefab, Vector3 position, Quaternion rotation, BossSettings settings)
    {
        GameObject bossObj = Object.Instantiate(prefab, position, rotation);

        bossObj.SetActive(true);
        
        IBoss boss = bossObj.GetComponent<IBoss>();
        if (boss == null)
        {
            boss = bossObj.AddComponent<Boss>();
        }
        
        boss.Initialize(settings);
        return boss;
    }
}