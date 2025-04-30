using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public AbstractBossState CurrentState { get; protected set; }
    public GameObject Boss { get; private set; }
    public Animator Animator { get; private set; }
    public BossSettings Settings { get; private set; }
    public BossVision Vision { get; private set; }
    public BossHealth Health { get; private set; }
    public BossMana Mana { get; private set; }
    public bool IsPeacefulMode { get; set; }
    public GameObject UIStatistics;

    private void Awake()
    {
        Boss = gameObject;
        Animator = GetComponent<Animator>();
        Settings = GetComponent<BossAI>().settings;
        Vision = GetComponent<BossVision>();
        Health = GetComponent<BossHealth>();
        Mana = GetComponent<BossMana>();

        ChangeState(new BossIdleState(this));
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

    public void ChangeState(AbstractBossState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}