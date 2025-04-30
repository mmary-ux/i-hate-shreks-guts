using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : AbstractBossState
{

    public BossIdleState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void AnimationUpdate() { }

    public override void PhysicsUpdate() { }

    public override void LogicUpdate()
    {
        if (_stateMachine.IsPeacefulMode)
        {
            return;
        }
        if (_stateMachine.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            _stateMachine.ChangeState(new BossAggressiveState(_stateMachine));
        }
    }
}