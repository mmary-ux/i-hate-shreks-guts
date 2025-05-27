using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBossSpell : ElementalWeapon
{
    [SerializeField] private float spellSpeed = 30f;
    [SerializeField] private float spellLifetime = 2f;
    [SerializeField] private int damage = 15;

    private const string SoundName = "MagicWhoosh";

    public void Init(GameObject prefab, float cooldown)
    {
        SpellPrefab = prefab;
        Cooldown = cooldown;
    }

    public override void Attack()
    {
        if (!canAttack) return;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;

        GameObject spell = Instantiate(SpellPrefab, transform.position, Quaternion.identity);
        spell.transform.LookAt(player.position);

        var controller = spell.AddComponent<SpellController>();
        controller.Init(spellSpeed, spellLifetime, damage, player);

        FindObjectOfType<AudioManager>().Play(SoundName);

        yield return new WaitForSeconds(Cooldown);
        canAttack = true;
    }
}
