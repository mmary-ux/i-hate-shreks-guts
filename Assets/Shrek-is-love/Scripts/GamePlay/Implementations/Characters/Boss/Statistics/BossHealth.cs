using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public Slider healthSlider;

    [SerializeField] private Animator animator;

    private BossStateMachine stateMachine;
    public bool isDead = false;

    [SerializeField] private int id;

    private void Awake()
    {
        stateMachine = GetComponent<BossAI>().stateMachine;
    }

    private void Start()
    {
        if (isDead) { gameObject.SetActive(false); return; }
        stateMachine = GetComponent<BossAI>().stateMachine;
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
        else 
        {
            if (stateMachine.IsPeacefulMode)
            {
                stateMachine.ChangeState(new BossAggressiveState(stateMachine));
            }
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("IsDead");
        EventManager.Instance?.EnemyKilled(EventManager.EnemyType.Boss);
        FindObjectOfType<AudioManager>().OffBossAttackMusic();
        CallAfterDelay.Create(3f, () =>
        {
            gameObject.SetActive(false);
            stateMachine.Boss.UIStatistics.SetActive(false);
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