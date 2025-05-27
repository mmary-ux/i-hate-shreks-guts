using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalWeapon : MonoBehaviour
{
    public float Cooldown { get; protected set; }
    public GameObject SpellPrefab { get; protected set; }
    public string SoundName { get; protected set; }

    protected Transform player;
    protected bool canAttack = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void Attack();
}
