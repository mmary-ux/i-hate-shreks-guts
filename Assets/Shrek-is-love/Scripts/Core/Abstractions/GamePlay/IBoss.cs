using UnityEngine;

public interface IBoss
{
    BossAI GetBossAI();
    void Initialize(BossSettings settings);
}