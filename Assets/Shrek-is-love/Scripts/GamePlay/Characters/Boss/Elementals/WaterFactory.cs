using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFactory : ElementalFactory
{
    public override ElementalWeapon CreateWeapon() => new WaterWeapon();
}
