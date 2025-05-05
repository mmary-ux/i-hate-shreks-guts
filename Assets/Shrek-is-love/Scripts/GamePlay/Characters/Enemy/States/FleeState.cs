using UnityEngine;
using UnityEngine.AI;

public class FleeState : AbstractEnemyState
{
    private Transform[] waypoints;
    private int currentWaypointIndex;

    public FleeState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        waypoints = _stateMachine.GetComponent<EnemyAI>().waypoints;
    }

    public override void Enter()
    {
        _stateMachine.Animator.SetBool("IsChasing", true);
        _stateMachine.Agent.isStopped = false;
        _stateMachine.Agent.speed = _stateMachine.Settings.speedRun * 1.5f;
        
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
            
            _stateMachine.Agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    public override void Exit()
    {
        _stateMachine.Animator.SetBool("IsChasing", false);
    }

    public override void AnimationUpdate()
    {
        _stateMachine.Animator.SetFloat("Speed", _stateMachine.Agent.velocity.magnitude);
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (_stateMachine.Health.currentHealth > _stateMachine.Health.maxHealth * 0.3f)
        {
            _stateMachine.ChangeState(new IdleState(_stateMachine));
            return;
        }
        if (waypoints.Length > 0 && 
            !_stateMachine.Agent.pathPending && 
            _stateMachine.Agent.remainingDistance <= _stateMachine.Agent.stoppingDistance + 0.1f)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        _stateMachine.Agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}