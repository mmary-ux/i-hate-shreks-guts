using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Weapon Settings")]
public class WeaponSettings : ScriptableObject
{
    public float attackRadius = 5f;
    public LayerMask playerLayer;
    public int damageAmount = 10;
    public float attackCooldown = 1.5f;
    public float knockbackForce = 1f;
}
