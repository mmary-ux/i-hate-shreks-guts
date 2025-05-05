using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDataPersistence
{
    public int maxHealth = 30;
    public int currentHealth;
    public Slider healthSlider; // Ссылка на UI Slider

    [SerializeField] private Animator animator;

    private EnemyStateMachine stateMachine;
    public bool isDead = false;

    [SerializeField] private int id;

    private void Awake() 
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void Start()
    {
        if (isDead) { gameObject.SetActive(false); return; }
        stateMachine = GetComponent<EnemyStateMachine>();
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
    public void LoadData(GameData gameData)
    {
        if (gameData.EnemyStatistics.TryGetValue(id, out EnemyData enemyData))
        {
            this.transform.position = enemyData.enemyPosition;
            currentHealth = enemyData.enemyHealth;
            isDead = enemyData.isDead;
            UpdateHealthUI();
            Debug.Log("ЗагруженнАЯ ПОЗИЦИЯ: " + enemyData.enemyPosition);
            Debug.Log("НАСТОЯЩАЯ ПОЗИЦИЯ: " + this.transform.position);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.EnemyStatistics[id] = new EnemyData(this.transform.position, currentHealth, isDead);
    }
}