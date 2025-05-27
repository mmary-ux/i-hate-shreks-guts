using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpellController : MonoBehaviour
{
    private float speed;
    private int damage;
    private Transform target;

    public void Init(float speed, float lifetime, int damage, Transform target)
    {
        this.speed = speed;
        this.damage = damage;
        this.target = target;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
