using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFactory : ElementalFactory
{
    public override GameObject GetSpellPrefab() => Resources.Load<GameObject>("WhiteFireEffects");
    public override string GetExplosionSound() => "AirAttack";
}
