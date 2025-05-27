using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<int> OnEnemyKilled;
    public event Action OnBossSpawn;
    public event Action OnVictory;
    public event Action OnBossFirstAttack;

    private int enemiesKilled = 0;
    public int enemiesMusic = 3;
    public int enemiesBoss = 5;

    private bool bossFirstAttackHappened = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        OnEnemyKilled?.Invoke(enemiesKilled);

        if (enemiesKilled == enemiesBoss)
        {
            OnBossSpawn?.Invoke();
        }
        else if (enemiesKilled == enemiesMusic)
        {
            OnVictory?.Invoke();
        }
    }

    public void BossFirstAttack()
    {
        if (!bossFirstAttackHappened)
        {
            bossFirstAttackHappened = true;
            OnBossFirstAttack?.Invoke();
        }
    }
}