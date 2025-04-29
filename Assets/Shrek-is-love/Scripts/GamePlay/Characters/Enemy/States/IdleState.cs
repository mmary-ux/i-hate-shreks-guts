// before: patrol state
using UnityEngine;
using UnityEngine.AI;

public class IdleState : AbstractEnemyState
{
    private Transform[] waypoints;
    private int currentWaypointIndex;
    private float waitTime;

    public IdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        waypoints = _stateMachine.GetComponent<EnemyAI>().waypoints;
    }

    public override void Enter()
    {
        waitTime = _stateMachine.Settings.startWaitTime;
        _stateMachine.Agent.isStopped = false;
        _stateMachine.Agent.speed = _stateMachine.Settings.speedWalk;
        
        if (waypoints.Length > 0)
        {
            _stateMachine.Agent.SetDestination(waypoints[currentWaypointIndex].position);
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
        // В мирном режиме проверяем только здоровье для перехода в бегство
        if (_stateMachine.IsPeacefulMode)
        {
            if (_stateMachine.Health.currentHealth <= _stateMachine.Health.maxHealth * 0.3f)
            {
                _stateMachine.ChangeState(new FleeState(_stateMachine));
                return;
            }
        }
        // В обычном режиме проверяем видимость игрока
        else if (_stateMachine.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            _stateMachine.ChangeState(new AggressiveState(_stateMachine));
            return;
        }

        // Логика патрулирования
        if (waypoints.Length == 0) return;

        if (!_stateMachine.Agent.pathPending && _stateMachine.Agent.remainingDistance <= _stateMachine.Agent.stoppingDistance + 0.1f)
        {
            if (!_stateMachine.Agent.isStopped)
            {
                _stateMachine.Agent.isStopped = true;
                waitTime = _stateMachine.Settings.startWaitTime;
            }

            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                NextPoint();
                _stateMachine.Agent.isStopped = false;
            }
        }
    }

    private void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        _stateMachine.Agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}