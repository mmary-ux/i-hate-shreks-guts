using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public Slider healthSlider;

    [SerializeField] private Animator animator;

    public bool isDead = false;

    [SerializeField] private int id;

    private void Start()
    {
        if (isDead) { gameObject.SetActive(false); return; }
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        animator.SetTrigger("IsHit");
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("IsDead");
        CallAfterDelay.Create(3f, () =>
        {
            gameObject.SetActive(false);
        });
    }

    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
}