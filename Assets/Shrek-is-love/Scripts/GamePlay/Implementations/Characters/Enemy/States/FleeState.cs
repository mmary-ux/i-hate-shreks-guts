using UnityEngine;
using UnityEngine.AI;

public class FleeState : AbstractEnemyState
{
    private Transform[] waypoints;
    private int currentWaypointIndex;

    public FleeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        waypoints = _stateMachine.Enemy.waypoints;
    }

    public override void Enter()
    {
        _stateMachine.Enemy.Animator.SetBool("IsChasing", true);
        _stateMachine.Enemy.Agent.isStopped = false;
        _stateMachine.Enemy.Agent.speed = _stateMachine.Enemy.Settings.speedRun * 1.5f;
        
        if (waypoints.Length > 0)
        {
            // Выбираем ближайший waypoint
            float minDistance = float.MaxValue;
            for (int i = 0; i < waypoints.Length; i++)
            {
                float distance = Vector3.Distance(_stateMachine.Enemy.transform.position, waypoints[i].position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentWaypointIndex = i;
                }
            }
            
            _stateMachine.Enemy.Agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    public override void Exit()
    {
        _stateMachine.Enemy.Animator.SetBool("IsChasing", false);
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
        if (_stateMachine.Enemy.Health.currentHealth > _stateMachine.Enemy.Health.maxHealth * 0.3f)
        {
            _stateMachine.ChangeState(new IdleState(_stateMachine));
            return;
        }
        if (waypoints.Length > 0 && 
            !_stateMachine.Enemy.Agent.pathPending && 
            _stateMachine.Enemy.Agent.remainingDistance <= _stateMachine.Enemy.Agent.stoppingDistance + 0.1f)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        _stateMachine.Enemy.Agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}