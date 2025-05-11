using UnityEngine;

public class BossStateMachine
{
    public BossAI Boss { get; private set; }
    public AbstractBossState CurrentState { get; protected set; }
    public bool IsPeacefulMode { get; set; }

    public BossStateMachine(BossAI boss)
    {
        Boss = boss;
    }

    public virtual void Initialize(BossAI boss)
    {
        CurrentState = new BossIdleState(this);
        CurrentState.Enter();
    }

    public void ChangeState(AbstractBossState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        newState.Enter();
    }

    public void SetPeacefulMode(bool peaceful)
    {
        IsPeacefulMode = peaceful;
        
        if (peaceful && !(CurrentState is BossIdleState))
        {
            ChangeState(new BossIdleState(this));
        }
        if (!peaceful && CurrentState is BossIdleState)
        {
            if (Boss.Vision.IsPlayerVisible(out Vector3 playerPosition))
            {
                ChangeState(new BossAggressiveState(this));
            }
        }
    }
}