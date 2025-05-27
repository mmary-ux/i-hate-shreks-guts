using UnityEngine;

public class EnemyStateMachine
{
    public EnemyAI Enemy { get; private set; }
    public AbstractEnemyState CurrentState { get; protected set; }
    public bool IsPeacefulMode { get; private set; }

    public EnemyStateMachine(EnemyAI enemy)
    {
        Enemy = enemy;
    }

    public virtual void Initialize(EnemyAI enemy)
    {
        CurrentState = new IdleState(this);
        CurrentState.Enter();
    }

    public void ChangeState(AbstractEnemyState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        newState.Enter();
    }
    
    public void SetPeacefulMode(bool peaceful)
    {
        IsPeacefulMode = peaceful;
        
        if (peaceful && Enemy.Health.currentHealth <= Enemy.Health.maxHealth * 0.3f)
        {
            ChangeState(new FleeState(this));
        }
        else if (!peaceful && CurrentState is IdleState)
        {
            if (Enemy.Vision.IsPlayerVisible(out Vector3 playerPosition))
            {
                ChangeState(new AggressiveState(this));
            }
        }
    }
}