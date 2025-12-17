using UnityEngine;
using UnityEngine.AI;

public class Enemy_TOL_Chase : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Line of Sight")]
    public LayerMask obstacleMask;
    public float sightRange = 10f;

    [Header("Chase")]
    public float chaseSpeed = 3.5f;
    public float stopDistance = 1.5f;

    private bool canSeePlayer = false;

    public int maxHealth = 2;
    private int currentHealth;

    public float attackRange = 2.0f;
    public int attackDamage = 1;
    public float attackRate = 1.5f;
    private float nextAttackTime;   
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        agent.stoppingDistance = stopDistance;
        if (agent != null)
        {
            agent.enabled = true; 
        }

        nextAttackTime = Time.time;

    }

    void Update()
    {
        if (player == null) return;

        CheckLineOfSight();
        if (canSeePlayer)
        {
            ChasePlayer();
        }
    }

    void CheckLineOfSight()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= sightRange)
        {
            // Lanseaza raycast pentru a verifica obstacolele
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
            {
                canSeePlayer = true;
                Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.green);
            }
            else
            {
                canSeePlayer = false;
                Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.red);
            }
        }
        else
        {
            canSeePlayer = false;
        }
    }

    void ChasePlayer()
{
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

    if (distanceToPlayer > attackRange)
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        
        Vector3 direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); 
    }
    else 
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
        
        if (Time.time > nextAttackTime)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage); 
                Debug.Log($"Inamicul ataca! Jucator lovit cu {attackDamage} daune.");
            } 
            nextAttackTime = Time.time + attackRate; 
        }
    }
}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Inamic lovit! HP ramas: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        // Aici poti adauga puncte, provizii, animatii, etc.
        Debug.Log("Inamic Distrus!");
        Destroy(gameObject);
    }


}