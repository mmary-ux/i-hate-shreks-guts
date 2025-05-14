using UnityEngine;
using UnityEngine.AI;

public class AttackState : AbstractEnemyState
{
    private Transform player;
    private float attackCooldown;
    private bool isAttacking;
    private DamageDealer damageDealer;
    private IMagicSpell magicSpell;
    private IWeapon weapon;

    public AttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        damageDealer = _stateMachine.Enemy.GetComponent<DamageDealer>();
        magicSpell = _stateMachine.Enemy.GetComponent<IMagicSpell>();
        weapon = _stateMachine.Enemy.GetComponent<IWeapon>();
    }

    public override void Enter()
    {
        _stateMachine.Enemy.Agent.isStopped = true;
        attackCooldown = _stateMachine.Enemy.Settings.attackWaitTime;
        isAttacking = false;
        TryAttack();
    }

    public override void Exit()
    {
        _stateMachine.Enemy.Animator.ResetTrigger("Attack");
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

        if (distance > _stateMachine.Enemy.Settings.attackRange * 1.2f)
        {
            _stateMachine.ChangeState(new AggressiveState(_stateMachine));
            return;
        }

        if (!_stateMachine.Enemy.Vision.IsPlayerVisible(out Vector3 playerPosition))
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
        if (_stateMachine.Enemy.Animator.GetBool("IsHit") || _stateMachine.Enemy.Animator.GetBool("IsDead"))
            return;

        isAttacking = true;
        attackCooldown = _stateMachine.Enemy.Settings.attackWaitTime;
        _stateMachine.Enemy.Animator.SetTrigger("Attack");

        if (_stateMachine.Enemy.CompareTag("BasicEnemy"))
        {
            CallAfterDelay.Create(0.55f, () =>
            {
                GameObject.FindObjectOfType<AudioManager>().Play("Punch");
                weapon?.Hit();
                isAttacking = false;
            });
        }
        else if (_stateMachine.Enemy.CompareTag("MagicEnemy"))
        {
            magicSpell?.CastSpell();
            isAttacking = false;
        }
    }
}