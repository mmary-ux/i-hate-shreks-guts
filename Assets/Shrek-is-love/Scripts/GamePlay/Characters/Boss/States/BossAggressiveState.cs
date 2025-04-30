using Unity.VisualScripting;
using UnityEngine;

public class BossAggressiveState : AbstractBossState
{
    private Transform player;
    private float attackTimer;
    private float forceFieldTimer;
    private GameObject currentForceField;
    private GameObject UIStatistics;

    public BossAggressiveState(BossStateMachine stateMachine) : base(stateMachine)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UIStatistics = stateMachine.UIStatistics;
    }

    public override void Enter()
    {
        UIStatistics.SetActive(true);
        attackTimer = _stateMachine.Settings.timeBetweenAttacks;
        forceFieldTimer = _stateMachine.Settings.forceFieldDuration;

        if (currentForceField == null)
        {
            CreateForceField();
        }
    }

    public override void Exit()
    {
        DestroyForceField();
    }

    public override void AnimationUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
        Vector3 lookPos = player.position - _stateMachine.Boss.transform.position;
        lookPos.y = 0;
        _stateMachine.Boss.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public override void LogicUpdate()
    {
        Debug.Log(_stateMachine.Mana.currentMana + " " + _stateMachine.Settings.manaForSpecialAttack);
        if (!_stateMachine.Vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            DestroyForceField();
            UIStatistics.SetActive(false);
            _stateMachine.ChangeState(new BossIdleState(_stateMachine));
            return;
        }

        forceFieldTimer -= Time.deltaTime;
        if (forceFieldTimer <= 0f)
        {
            DestroyForceField();
            forceFieldTimer = _stateMachine.Settings.forceFieldDuration;
            CreateForceField();
        }

        attackTimer -= Time.deltaTime;
        _stateMachine.Mana.currentMana += Time.deltaTime;
        if (attackTimer <= 0f)
        {
            if (_stateMachine.Mana.currentMana >= _stateMachine.Settings.manaForSpecialAttack)
            {
                _stateMachine.ChangeState(new BossSpecialAttackState(_stateMachine));
                _stateMachine.Mana.currentMana = 0;
            }
            else
            {
                _stateMachine.ChangeState(new BossAttackState(_stateMachine));
            }
        }
    }

    private void CreateForceField()
    {
        Debug.Log("The Field created/.");
        if (currentForceField != null) return;

        currentForceField = GameObject.Instantiate(
            _stateMachine.Settings.forceFieldPrefab,
            player.position,
            Quaternion.identity
        );

        SpawnMinions();
    }

    private void DestroyForceField()
    {
        if (currentForceField != null)
        {
            GameObject.Destroy(currentForceField);
            currentForceField = null;
        }
    }

    private void SpawnMinions()
    {
        for (int i = 0; i < _stateMachine.Settings.minionsToSpawn; i++)
        {
            Vector3 spawnPos = player.position + Random.insideUnitSphere * _stateMachine.Settings.spawnRadius;
            GameObject.Instantiate(
                _stateMachine.Settings.minionPrefab,
                spawnPos,
                Quaternion.identity
            );
        }
    }
}