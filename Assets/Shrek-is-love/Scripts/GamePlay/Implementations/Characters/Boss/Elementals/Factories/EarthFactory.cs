using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFactory : ElementalFactory
{
    public override GameObject GetSpellPrefab() => Resources.Load<GameObject>("GreenFireEffects");
    public override string GetExplosionSound() => "EarthAttack";
}
