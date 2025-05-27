using System;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<EnemyType, int> OnEnemyKilled;
    public event Action OnBossSpawn;
    public event Action OnVictory;
    public event Action OnBossFirstAttack;

    public enum EnemyType { Regular, Fairy, Boss }

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

    public void EnemyKilled(EnemyType enemyType)
    {
        enemiesKilled++;
        OnEnemyKilled?.Invoke(enemyType, enemiesKilled);

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