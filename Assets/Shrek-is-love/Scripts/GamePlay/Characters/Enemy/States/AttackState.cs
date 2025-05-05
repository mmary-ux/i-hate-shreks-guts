using UnityEngine;
using UnityEngine.AI;

public class AttackState : AbstractEnemyState
{
    private Transform player;
    private float attackCooldown;
    private bool isAttacking;
    private DamageDealer damageDealer;
    private MagicSpell magicSpell;

    public AttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        damageDealer = _stateMachine.GetComponent<DamageDealer>();
        magicSpell = _stateMachine.GetComponent<MagicSpell>();
    }

    public override void Enter()
    {
        _stateMachine.Agent.isStopped = true;
        attackCooldown = _stateMachine.Settings.attackWaitTime;
        isAttacking = false;
        TryAttack();
    }

    public override void Exit()
    {
        _stateMachine.Animator.ResetTrigger("Attack");
    }

    public override void AnimationUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
        Vector3 playerPos = player.position;
        _stateMachine.Enemy.transform.LookAt(new Vector3(playerPos.x, _stateMachine.Enemy.transform.position.y, playerPos.z));
    }

    public override void LogicUpdate()
    {
        float distance = Vector3.Distance(_stateMachine.Enemy.transform.position, player.position);

        if (distance > _stateMachine.Settings.attackRange * 1.2f)
        {
            _stateMachine.ChangeState(new AggressiveState(_stateMachine));
            return;
        }

        if (!_stateMachine.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            _stateMachine.ChangeState(new IdleState(_stateMachine));
            return;
        }

        // Кулдаун атаки
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else if (!isAttacking)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (_stateMachine.Animator.GetBool("IsHit") || _stateMachine.Animator.GetBool("IsDead"))
            return;

        isAttacking = true;
        attackCooldown = _stateMachine.Settings.attackWaitTime;
        _stateMachine.Animator.SetTrigger("Attack");

        if (_stateMachine.Enemy.CompareTag("BasicEnemy"))
        {
            CallAfterDelay.Create(0.55f, () =>
            {
                GameObject.FindObjectOfType<AudioManager>().Play("Punch");
                damageDealer?.Attack();
                isAttacking = false;
            });
        }
        else if (_stateMachine.Enemy.CompareTag("MagicEnemy"))
        {
            magicSpell.Enter();
            isAttacking = false;
        }
    }
}