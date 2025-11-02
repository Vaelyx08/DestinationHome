using UnityEngine;

public class PlayerLineOfSight : MonoBehaviour
{
    public Transform target;       // Inamicul
    public LayerMask obstacleMask; // Layer pentru obstacole (ex: pereti)
    public float sightRange = 10f; // Distanta maxima de vizibilitate

    void Update()
    {
        CheckLineOfSight();
    }

    void CheckLineOfSight()
    {
        if (target == null) return;

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Verifica daca e in raza de vedere
        if (distanceToTarget <= sightRange)
        {
            // Lanseaza o raza din player spre target
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
            {
                // Niciun obstacol -> linie de vedere libera
                Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.green);
                Debug.Log("Target in sight!");
            }
            else
            {
                // Obstacol detectat -> blocat
                Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.red);
                Debug.Log("Line of sight blocked!");
            }
        }
    }
}
