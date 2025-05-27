using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour, IWeapon
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
                    StartCoroutine(KnockbackCoroutine(hitCollider.transform));
                    Debug.Log("Игрок получил урон: " + weaponSettings.damageAmount);
                }
            }
        }

        yield return new WaitForSeconds(weaponSettings.attackCooldown);
        canAttack = true;
    }

    private IEnumerator KnockbackCoroutine(Transform playerTransform)
    {
        Vector3 startPos = playerTransform.position;
        Vector3 knockbackDirection = (playerTransform.position - transform.position).normalized;
        Vector3 targetPos = startPos + knockbackDirection * weaponSettings.knockbackForce;

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            playerTransform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
