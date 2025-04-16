using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private new Transform camera;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator animator;

    [SerializeField] private float minimumSpeed = 15f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private float turnSmoothTime = 0.25f;
    float turnSmoothVelocity;

    Vector3 velocity;

    [SerializeField] private float groundDistance = 0.65f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canMove = true;

    [SerializeField] private int physAttackPoints = 5;
    [SerializeField] private float physAttackRadius = 5f;
    [SerializeField] private int yellAttackPoints = 2;
    [SerializeField] private float yellAttackRadius = 10f;
    [SerializeField] private int manaPoints = 5;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Gravity();
        Attack();
        Jump();

        canMove = false;

        // Проверяем завершение анимации атаки и крика
        if (!IsAnimationPlaying("Attacking") && !IsAnimationPlaying("Yelling"))
        {
            canMove = true;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isGrounded)
        {
            inputMagnitude *= 2;
        }

        float speed = inputMagnitude * minimumSpeed;

        if (direction.magnitude >= 0.1f && canMove) 
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y; 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
        animator.SetFloat("inputMagnitude", inputMagnitude, 0.1f, Time.deltaTime);
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // ������� ��������� ����� �������� Distance � ������� �������, ��������� �������� �� ����� � Mask 
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded && canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Console.WriteLine("jump");
            animator.SetBool("isJumping", true);

        }

        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !IsAnimationPlaying("Attacking") && !IsAnimationPlaying("Yelling") && isGrounded)
        {
            canMove = false;
            animator.SetTrigger("Attack");
            CallAfterDelay.Create(0.5f, () =>
            {
                DealDamageToEnemies(physAttackPoints, "Attack");
            });
            
        }

        if (Input.GetMouseButtonDown(1) && !IsAnimationPlaying("Yelling") && !IsAnimationPlaying("Attacking") && isGrounded)
        {
            canMove = false;
            ManaSystem manaSystem = GetComponent<ManaSystem>();
            if (manaSystem != null && manaSystem.GetCurrentMana() > 0)
            {
                if (manaSystem.UseSufficientMana(manaPoints))
                {
                    animator.SetTrigger("Yell");
                    FindObjectOfType<AudioManager>().Play("Scream");
                    
                    DealDamageToEnemies(yellAttackPoints, "Yell");
                }
            }
        }
    }

    private void DealDamageToEnemies(int damage, string attackType)
    {
        float attackRadius = (attackType == "Attack") ? 5f : 10f; 
        LayerMask enemyLayer = LayerMask.GetMask("Enemy"); 

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);
        
        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Нанесено {damage} урона врагу {enemy.name} (тип атаки: {attackType})");
            }
        }
    }

    // Переменная, вызывающаяся в аниматоре
    // Не используется
    public void EnableMovement()
    {
        canMove = true;
    }

    // Метод для проверки, проигрывается ли анимация
    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isPlaying = stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f;
        return isPlaying;
    }
}
