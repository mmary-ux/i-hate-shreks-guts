using UnityEngine;

public abstract class AbstractEnemyState
{
    protected EnemyStateMachine _stateMachine;

    public AbstractEnemyState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public abstract void AnimationUpdate();
    public abstract void PhysicsUpdate();
    public abstract void LogicUpdate();
}