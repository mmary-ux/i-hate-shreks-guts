using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloudController : MonoBehaviour
{
    private int poisonDamagePerSecond;

    public void Init(int cloudDuration, int poisonDamagePerSecond)
    {
        this.poisonDamagePerSecond = poisonDamagePerSecond;
        Destroy(gameObject, cloudDuration);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(poisonDamagePerSecond);
        }
    }
}
