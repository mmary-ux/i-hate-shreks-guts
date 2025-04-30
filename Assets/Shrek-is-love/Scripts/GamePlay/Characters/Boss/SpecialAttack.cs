using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private float explosionGrowthRate = 10f;
    [SerializeField] private float maxExplosionSize = 30f;
    [SerializeField] private float spellDuration = 5f;

    private GameObject currentSpell;
    private bool isSpellActive;
    private float currentSpellTimer;
    private DamageDealer damageDealer;

    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSpellTimer = spellDuration;
    }

    public void Enter()
    {
        if (!isSpellActive)
        {
            currentSpell = Instantiate(spellPrefab, player.transform.position, Quaternion.identity);
            currentSpell.transform.localScale = Vector3.one * 0.1f;

            damageDealer = currentSpell.GetComponent<DamageDealer>();
            isSpellActive = true;
            currentSpellTimer = spellDuration;
        }
    }

    private void Update()
    {
        if (!isSpellActive) return;

        currentSpellTimer -= Time.deltaTime;

        if (currentSpell != null)
        {
            if (currentSpell.transform.localScale.x < maxExplosionSize)
            {
                float newSize = currentSpell.transform.localScale.x + explosionGrowthRate * Time.deltaTime;
                currentSpell.transform.localScale = Vector3.one * newSize;
            }

            float distanceToPlayer = Vector3.Distance(currentSpell.transform.position,
                player.transform.position);

            if (distanceToPlayer < currentSpell.transform.localScale.x / 2f)
            {
                damageDealer.Attack();
            }
        }

        if (currentSpellTimer <= 0f)
        {
            Destroy(currentSpell);
        }
    }
}