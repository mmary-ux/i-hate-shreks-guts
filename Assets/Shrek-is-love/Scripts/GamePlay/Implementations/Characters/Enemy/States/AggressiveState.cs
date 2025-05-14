using UnityEngine;
using UnityEngine.AI;

public class AggressiveState : AbstractEnemyState
{
    private Transform player;
    private bool playerLost;

    public AggressiveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        _stateMachine.Enemy.Animator.SetBool("IsChasing", true);
        _stateMachine.Enemy.Agent.isStopped = false;
        _stateMachine.Enemy.Agent.speed = _stateMachine.Enemy.Settings.speedRun;
        playerLost = false;
    }

    public override void Exit()
    {
        if (playerLost)
        {
            Debug.Log("Игрок потерян, возвращаюсь на патрулирование");
        }
    }

    public override void AnimationUpdate()
    {
        _stateMachine.Enemy.Animator.SetFloat("Speed", _stateMachine.Enemy.Agent.velocity.magnitude);
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        float distanceToPlayer = Vector3.Distance(_stateMachine.Enemy.transform.position, player.position);

        if (distanceToPlayer <= _stateMachine.Enemy.Settings.attackRange)
        {
            _stateMachine.ChangeState(new AttackState(_stateMachine));
            return;
        }

        if (!_stateMachine.Enemy.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            playerLost = true;
            _stateMachine.ChangeState(new IdleState(_stateMachine));
            return;
        }

        _stateMachine.Enemy.Agent.SetDestination(player.position);
    }
}