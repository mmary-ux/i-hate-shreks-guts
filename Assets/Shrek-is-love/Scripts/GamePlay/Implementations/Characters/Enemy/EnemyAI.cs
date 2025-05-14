using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySettings settings;
    public Transform[] waypoints;
    public bool isPeacefulMode = false;

    public EnemyStateMachine stateMachine { get; private set; }

    public GameObject Enemy { get; private set; }
    public UnityEngine.AI.NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    public EnemySettings Settings { get; private set; }
    public EnemyVision Vision { get; private set; }
    public EnemyHealth Health { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Settings = settings;
        Vision = GetComponent<EnemyVision>();
        Health = GetComponent<EnemyHealth>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.Initialize(this);

        if (GameSettingsManager.Instance != null)
        {
            SetPeacefulMode(GameSettingsManager.Instance.PeacefulModeEnabled);
        }
        else
        {
            Debug.LogWarning("GameSettingsManager not found, using default peaceful mode");
            SetPeacefulMode(false);
        }
    }

    private void Update()
    {
        stateMachine?.CurrentState.LogicUpdate();
        stateMachine?.CurrentState.AnimationUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine?.CurrentState.PhysicsUpdate();
    }

    public void SetPeacefulMode(bool peaceful)
    {
        isPeacefulMode = peaceful;
        stateMachine?.SetPeacefulMode(peaceful);
    }
}