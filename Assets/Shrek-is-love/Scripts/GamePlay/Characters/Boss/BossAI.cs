using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public BossSettings settings;

    private BossStateMachine stateMachine;
    private BossVision vision;
    private BossHealth health;
    private BossMana mana;
    public bool isPeacefulMode = false;

    private void Awake()
    {
        stateMachine = GetComponent<BossStateMachine>();
        vision = GetComponent<BossVision>();
        health = GetComponent<BossHealth>();
        mana = GetComponent<BossMana>();

        if (stateMachine != null)
        {
            stateMachine.IsPeacefulMode = isPeacefulMode;
        }
    }

    private void Start()
    {
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

    public void SetPeacefulMode(bool peaceful)
    {
        isPeacefulMode = peaceful;
        if (stateMachine != null)
        {
            stateMachine.IsPeacefulMode = peaceful;
            
            if (peaceful && !(stateMachine.CurrentState is BossIdleState))
            {
                stateMachine.ChangeState(new BossIdleState(stateMachine));
            }
            if (!peaceful && stateMachine.CurrentState is BossIdleState)
            {
                if (vision.IsPlayerVisible(out Vector3 playerPosition))
                {
                    stateMachine.ChangeState(new BossAggressiveState(stateMachine));
                }
            }
        }
    }
}