using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFactory : ElementalFactory
{
    public override ElementalWeapon CreateWeapon() => new FireWeapon();
}
