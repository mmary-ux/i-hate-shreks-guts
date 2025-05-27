using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float lifetime = 0.5f;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        DealDamage();
        Destroy(gameObject, lifetime);
    }

    private void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                HealthSystem health = hitCollider.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}
