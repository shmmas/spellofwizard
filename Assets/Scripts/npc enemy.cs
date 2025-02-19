using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINPC : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 100f;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;
    public GameObject projectile;

    // States
    public float sightRange = 10f, attackRange = 3f;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform; // Ensure the player object is named "PlayerObj"
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for player in sight range and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            navMeshAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position); // Chase the player
    }

    private void AttackPlayer()
    {
        // Stop moving when attacking
        navMeshAgent.SetDestination(transform.position);

        // Make the NPC face the player
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Instantiate the projectile and shoot it
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);  // Force in forward direction (towards player)
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);  // Force upwards to make it fly

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);  // Reset the attack after the cooldown
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);  // Destroy NPC when health reaches 0
    }

    private void OnDrawGizmosSelected()
    {
        // Draw sight and attack ranges for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

