using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicSpell : MonoBehaviour, IMagicSpell
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private float spellSpeed = 30f;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] private float spellLifetime = 2f;
    [SerializeField] private int damage = 15;

    private Transform player;
    private bool canCast = true;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void CastSpell()
    {
        if (!canCast) return;

        StartCoroutine(SpellCastCoroutine());
    }

    private IEnumerator SpellCastCoroutine()
    {
        canCast = false;

        GameObject spell = Instantiate(spellPrefab, transform.position, Quaternion.identity);
        spell.transform.LookAt(player.position);

        var spellController = spell.AddComponent<SpellController>();
        spellController.Init(spellSpeed, spellLifetime, damage, player);

        FindObjectOfType<AudioManager>().Play("MagicWhoosh");

        yield return new WaitForSeconds(cooldown);
        canCast = true;
    }

    public float Cooldown => cooldown;
}