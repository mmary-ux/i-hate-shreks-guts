// before: chase state
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
        _stateMachine.Animator.SetBool("IsChasing", true);
        _stateMachine.Agent.isStopped = false;
        _stateMachine.Agent.speed = _stateMachine.Settings.speedRun;
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
        _stateMachine.Animator.SetFloat("Speed", _stateMachine.Agent.velocity.magnitude);
    }

    public override void PhysicsUpdate()
    {
        // Физика перемещения обрабатывается NavMeshAgent
    }

    public override void LogicUpdate()
    {
        float distanceToPlayer = Vector3.Distance(_stateMachine.Enemy.transform.position, player.position);

        // Если игрок близко - атакуем
        if (distanceToPlayer <= _stateMachine.Settings.attackRange)
        {
            _stateMachine.ChangeState(new AttackState(_stateMachine));
            return;
        }

        // Если игрок вне поля зрения - возвращаемся в состояние покоя
        if (!_stateMachine.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            playerLost = true;
            _stateMachine.ChangeState(new IdleState(_stateMachine));
            return;
        }

        // Продолжаем преследование
        _stateMachine.Agent.SetDestination(player.position);
    }
}