using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttackState : AbstractBossState
{
    private Transform player;
    private bool hasAttacked;
    private SpecialAttack specialAttack;

    public BossSpecialAttackState(BossStateMachine stateMachine) : base(stateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        specialAttack = stateMachine.GetComponent<SpecialAttack>();
    }

    public override void Enter()
    {
        hasAttacked = false;
        _stateMachine.Animator.SetTrigger("SpecialAttack");
        _stateMachine.Mana.UseMana(_stateMachine.Settings.manaForSpecialAttack);
        specialAttack.Enter();
    }

    public override void Exit()
    {
        _stateMachine.Animator.ResetTrigger("SpecialAttack");
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
        if (!hasAttacked && _stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            hasAttacked = true;

            _stateMachine.ChangeState(new BossAggressiveState(_stateMachine));
        }
    }
}