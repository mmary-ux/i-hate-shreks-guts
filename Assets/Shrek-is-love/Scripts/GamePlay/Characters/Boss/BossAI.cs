using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public BossSettings settings;
    public bool isPeacefulMode = false;

    public BossStateMachine stateMachine { get; private set; }

    public GameObject Boss { get; private set; }
    public Animator Animator { get; private set; }
    public BossSettings Settings { get; private set; }
    public BossVision Vision { get; private set; }
    public BossHealth Health { get; private set; }
    public BossMana Mana { get; private set; }
    public GameObject UIStatistics;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Settings = settings;
        Vision = GetComponent<BossVision>();
        Health = GetComponent<BossHealth>();
        Mana = GetComponent<BossMana>();

        stateMachine = new BossStateMachine(this);
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