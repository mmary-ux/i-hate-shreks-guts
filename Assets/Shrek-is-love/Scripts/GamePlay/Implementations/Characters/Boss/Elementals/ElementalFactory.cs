using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalFactory
{
    public abstract GameObject GetSpellPrefab();
    public abstract string GetExplosionSound();
}
