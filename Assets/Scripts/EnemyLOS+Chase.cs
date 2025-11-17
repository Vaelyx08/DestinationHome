using UnityEngine;

public class Enemy_TOL_Chase : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Line of Sight")]
    public LayerMask obstacleMask;
    public float sightRange = 10f;

    [Header("Chase")]
    public float chaseSpeed = 3.5f;
    public float stopDistance = 2f;

    private bool canSeePlayer = false;

    void Update()
    {
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
        if (distanceToPlayer > stopDistance)
        {
            // Misca si roteste inamicul spre player
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            Vector3 direction = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }

    }
}