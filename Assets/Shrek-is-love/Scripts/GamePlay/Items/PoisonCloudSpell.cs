using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloudSpell : MonoBehaviour, IMagicSpell
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private int cloudDuration = 5;
    [SerializeField] private int poisonDamagePerSecond = 7;

    public void CastSpell()
    {
        GameObject cloud = Instantiate(cloudPrefab, transform.position, Quaternion.identity);
        cloud.GetComponent<PoisonCloudController>().Init(cloudDuration, poisonDamagePerSecond);
    }

    public float Cooldown => 8f;
}