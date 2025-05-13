using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFactory : ElementalFactory
{
    public override ElementalWeapon CreateWeapon() => new EarthWeapon();
}
