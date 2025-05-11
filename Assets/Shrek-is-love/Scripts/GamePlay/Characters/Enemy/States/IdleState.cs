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
        waypoints = _stateMachine.Enemy.waypoints;
    }

    public override void Enter()
    {
        waitTime = _stateMachine.Enemy.Settings.startWaitTime;
        _stateMachine.Enemy.Agent.isStopped = false;
        _stateMachine.Enemy.Agent.speed = _stateMachine.Enemy.Settings.speedWalk;
        
        if (waypoints.Length > 0)
        {
            _stateMachine.Enemy.Agent.SetDestination(waypoints[currentWaypointIndex].position);
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
        if (_stateMachine.IsPeacefulMode)
        {
            if (_stateMachine.Enemy.Health.currentHealth <= _stateMachine.Enemy.Health.maxHealth * 0.3f)
            {
                _stateMachine.ChangeState(new FleeState(_stateMachine));
                return;
            }
        }

        else if (_stateMachine.Enemy.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            _stateMachine.ChangeState(new AggressiveState(_stateMachine));
            return;
        }

        if (waypoints.Length == 0) return;

        if (!_stateMachine.Enemy.Agent.pathPending && _stateMachine.Enemy.Agent.remainingDistance <= _stateMachine.Enemy.Agent.stoppingDistance + 0.1f)
        {
            if (!_stateMachine.Enemy.Agent.isStopped)
            {
                _stateMachine.Enemy.Agent.isStopped = true;
                waitTime = _stateMachine.Enemy.Settings.startWaitTime;
            }

            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                NextPoint();
                _stateMachine.Enemy.Agent.isStopped = false;
            }
        }
    }

    private void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        _stateMachine.Enemy.Agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}