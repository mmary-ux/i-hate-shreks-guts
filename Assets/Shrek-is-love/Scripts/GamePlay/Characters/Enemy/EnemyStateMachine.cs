using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public AbstractEnemyState CurrentState { get; protected set; }
    public GameObject Enemy { get; private set; }
    public UnityEngine.AI.NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    public EnemySettings Settings { get; private set; }
    public EnemyVision Vision { get; private set; }
    public EnemyHealth Health { get; private set; }
    public bool IsPeacefulMode { get; set; }

    private void Awake()
    {
        Enemy = gameObject;
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Settings = GetComponent<EnemyAI>().settings;
        Vision = GetComponent<EnemyVision>();
        Health = GetComponent<EnemyHealth>();

        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        CurrentState?.LogicUpdate();
        CurrentState?.AnimationUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }

    public void ChangeState(AbstractEnemyState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}