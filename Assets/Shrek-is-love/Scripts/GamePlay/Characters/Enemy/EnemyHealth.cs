using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public Slider healthSlider; // Ссылка на UI Slider

    [SerializeField] private Animator animator;

    private EnemyStateMachine stateMachine;
    public bool isDead = false;

    private void Awake() 
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void Start()
    {
        if (isDead) { gameObject.SetActive(false); return; }
        stateMachine = GetComponent<EnemyStateMachine>();
        currentHealth = maxHealth;
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
            if (!stateMachine.IsPeacefulMode)
            {
                stateMachine.ChangeState(new AggressiveState(stateMachine));
            }
            else if (currentHealth <= maxHealth * 0.3f)
            {
                stateMachine.ChangeState(new FleeState(stateMachine));
            }
        }
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