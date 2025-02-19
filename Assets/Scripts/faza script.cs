using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform head; // Reference to the head transform
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public int health = 100;
    public float attackCooldown = 1.5f; // Cooldown between attacks

    private Animator animator;
    private Rigidbody rb;
    private bool isKnockedOut = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // If Rigidbody is not found in root, look in children (for ragdoll)
        if (rb == null)
        {
            rb = GetComponentInChildren<Rigidbody>();
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found in Enemy or child objects!");
            return;
        }

        rb.freezeRotation = true; // Prevent unwanted rotation

        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
        }

        ActivateRagdoll(false); // Disable ragdoll at start
    }

    void Update()
    {
        if (isKnockedOut || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange && !isAttacking)
        {
            MoveTowardsPlayer();
        }
        else if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetBool("IsWalking", false);
            StartCoroutine(Attack());
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        // Calculate the direction towards the player, keeping y = 0 to stay on the ground
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            // Smoothly rotate towards the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            // If head is assigned, rotate it to look at the player
            if (head != null)
            {
                head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }

        // Move the AI towards the player
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Update animator parameters for movement
        float forwardAmount = Vector3.Dot(transform.forward, direction);  // How much it's moving forward/backward
        float rightAmount = Vector3.Dot(transform.right, direction);     // How much it's moving left/right

        // Set the animation parameters for walking
        animator.SetFloat("Vertical", forwardAmount);  // Forward movement (1 for forward, -1 for backward)
        animator.SetFloat("Horizontal", rightAmount);  // Sideways movement (1 for right, -1 for left)
        animator.SetBool("IsWalking", true);  // Start walking animation
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // Ensure AI faces the player before attacking
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        // If head is assigned, rotate it to look at the player
        if (head != null)
        {
            head.rotation = Quaternion.Slerp(head.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Use attack animation
        int attackType = Random.Range(0, 2); // 0 = Jab, 1 = Hook
        if (attackType == 0)
        {
            Debug.Log("Enemy is punching: Jab");
            animator.SetTrigger("Jab");
        }
        else
        {
            Debug.Log("Enemy is punching: Hook");
            animator.SetTrigger("Hook");
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        if (isKnockedOut) return;

        health -= damage;
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Knockout();
        }
    }

    void Knockout()
    {
        isKnockedOut = true;
        animator.enabled = false;
        rb.isKinematic = true;

        ActivateRagdoll(true);
        Destroy(this, 5f);
    }

    void ActivateRagdoll(bool state)
    {
        Rigidbody[] ragdollParts = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody part in ragdollParts)
        {
            part.isKinematic = !state;
        }

        if (!state)
        {
            animator.enabled = true;
            rb.isKinematic = false;
        }
    }
}