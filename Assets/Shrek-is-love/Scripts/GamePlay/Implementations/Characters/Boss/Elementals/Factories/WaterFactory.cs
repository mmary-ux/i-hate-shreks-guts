using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFactory : ElementalFactory
{
    public override GameObject GetSpellPrefab() => Resources.Load<GameObject>("BlueFireEffects");
    public override string GetExplosionSound() => "WaterAttack";
}
