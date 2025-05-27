using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ElementalWeapon
{
    private GameObject explosionPrefab;
    public void Init(string sound, float cooldown)
    {
        SoundName = sound;
        Cooldown = cooldown;
        explosionPrefab = Resources.Load<GameObject>("Explosion");
    }

    public override void Attack()
    {
        if (!canAttack) return;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;

        GameObject explosion = Instantiate(explosionPrefab, player.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play(SoundName);

        yield return new WaitForSeconds(Cooldown);
        canAttack = true;
    }
}
