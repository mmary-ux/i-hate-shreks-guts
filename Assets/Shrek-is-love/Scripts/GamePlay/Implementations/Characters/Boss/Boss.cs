using UnityEngine;

public class Boss : MonoBehaviour, IBoss
{
    private BossAI bossAI;

    public BossAI GetBossAI() => bossAI;

    public void Initialize(BossSettings settings)
    {
        bossAI = GetComponent<BossAI>();
        if (bossAI == null)
        {
            bossAI = gameObject.AddComponent<BossAI>();
        }
        bossAI.settings = settings;
    }
}