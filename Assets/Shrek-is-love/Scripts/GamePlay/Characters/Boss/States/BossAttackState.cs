using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : AbstractBossState
{
    private Transform player;
    private bool hasAttacked;
    private MagicSpell magicSpell;

    public BossAttackState(BossStateMachine stateMachine) : base(stateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        magicSpell = _stateMachine.Boss.GetComponent<MagicSpell>();
    }

    public override void Enter()
    {
        hasAttacked = false;
        _stateMachine.Boss.Animator.SetTrigger("Attack");
        magicSpell.Enter();
    }

    public override void Exit()
    {
        _stateMachine.Boss.Animator.ResetTrigger("Attack");
    }

    public override void AnimationUpdate() { }

    public override void PhysicsUpdate()
    {
        Vector3 lookPos = player.position - _stateMachine.Boss.transform.position;
        lookPos.y = 0;
        _stateMachine.Boss.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public override void LogicUpdate()
    {
        if (!hasAttacked && _stateMachine.Boss.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            hasAttacked = true;
            _stateMachine.ChangeState(new BossAggressiveState(_stateMachine));
        }
    }
}