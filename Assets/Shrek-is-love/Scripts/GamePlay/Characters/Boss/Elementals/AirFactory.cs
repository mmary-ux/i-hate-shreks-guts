using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFactory : ElementalFactory
{
    public override ElementalWeapon CreateWeapon() => new AirWeapon();
}
