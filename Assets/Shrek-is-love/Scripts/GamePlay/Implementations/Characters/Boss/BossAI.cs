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

    private ElementalWeapon currentWeapon;
    private ElementalFactory elementFactory;

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

        elementFactory = GetRandomElementFactory();

        if (Random.Range(0, 2) == 0)
        {
            var weapon = gameObject.AddComponent<Explosion>();
            weapon.Init(
                elementFactory.GetExplosionSound(),
                3f
            );
            currentWeapon = weapon;
        }
        else
        {
            var weapon = gameObject.AddComponent<BasicBossSpell>();
            weapon.Init(
                elementFactory.GetSpellPrefab(),
                1.5f
            );
            currentWeapon = weapon;
        }

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

    private ElementalFactory GetRandomElementFactory()
    {
        int randomIndex = Random.Range(0, 4);
        return randomIndex switch
        {
            0 => new FireFactory(),
            1 => new WaterFactory(),
            2 => new EarthFactory(),
            3 => new AirFactory(),
            _ => new FireFactory()
        };
    }

    public void PerformAttack()
    {
        currentWeapon?.Attack();
        EventManager.Instance?.BossFirstAttack();
    }
}