using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySettings settings;
    public Transform[] waypoints;
    public bool isPeacefulMode = false;

    private EnemyStateMachine stateMachine;
    private EnemyVision vision;
    private EnemyHealth health;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        vision = GetComponent<EnemyVision>();
        health = GetComponent<EnemyHealth>();
        
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
            
            if (peaceful && health.currentHealth <= health.maxHealth * 0.3f)
            {
                stateMachine.ChangeState(new FleeState(stateMachine));
            }
            else if (!peaceful && stateMachine.CurrentState is IdleState)
            {
                if (vision.IsPlayerVisible(out Vector3 playerPosition))
                {
                    stateMachine.ChangeState(new AggressiveState(stateMachine));
                }
            }
        }
    }
}