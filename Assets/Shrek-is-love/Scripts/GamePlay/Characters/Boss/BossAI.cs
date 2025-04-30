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

    private void Awake()
    {
        stateMachine = GetComponent<BossStateMachine>();
        vision = GetComponent<BossVision>();
        health = GetComponent<BossHealth>();
        mana = GetComponent<BossMana>();
    }

    private void Start()
    {
        stateMachine.ChangeState(new BossIdleState(stateMachine));
    }
}