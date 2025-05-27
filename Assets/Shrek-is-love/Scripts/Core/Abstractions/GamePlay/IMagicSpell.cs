using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagicSpell
{
    void CastSpell(); 
    float Cooldown { get; }
}

