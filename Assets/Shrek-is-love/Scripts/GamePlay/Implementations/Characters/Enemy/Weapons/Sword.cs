using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public WeaponSettings weaponSettings;
    private bool canAttack = true;

    public void Hit()
    {
        if (!canAttack) return;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, weaponSettings.attackRadius, weaponSettings.playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                HealthSystem playerHealth = hitCollider.GetComponent<HealthSystem>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(weaponSettings.damageAmount);
                    Debug.Log("Игрок получил урон: " + weaponSettings.damageAmount);
                }
            }
        }

        yield return new WaitForSeconds(weaponSettings.attackCooldown);
        canAttack = true;
    }
}
