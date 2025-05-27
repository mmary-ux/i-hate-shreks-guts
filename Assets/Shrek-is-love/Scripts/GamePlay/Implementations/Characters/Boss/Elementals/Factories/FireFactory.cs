using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFactory : ElementalFactory
{
    public override GameObject GetSpellPrefab() => Resources.Load<GameObject>("RedFireEffects");
    public override string GetExplosionSound() => "FireAttack";
}
