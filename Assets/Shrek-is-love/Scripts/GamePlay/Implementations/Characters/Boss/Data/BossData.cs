using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossData
{
    public Vector3 bossPosition;
    public int bossHealth;
    public bool isDead;
    public BossData(Vector3 pos, int health, bool isDead)
    {
        bossPosition = pos;
        bossHealth = health;
        this.isDead = isDead;
    }
}