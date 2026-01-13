using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageAmount = 1f;
    public bool isPlayerBullet = false; // Flag pentru a sti cine a tras
    
    // Asigura-te ca GameObject-ul glontului are un Collider setat pe IsTrigger = true
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player") && isPlayerBullet)
        {
            return;
        }

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        Enemy_TOL_Chase enemyHealth = other.GetComponent<Enemy_TOL_Chase>(); // Presupunem ca inamicii au un script numit EnemyController

        if (playerHealth != null && !isPlayerBullet)
        {
            // Glont inamic loveste jucatorul
            playerHealth.TakeDamage((int)damageAmount); 
            Destroy(gameObject); 
        }
        else if (enemyHealth != null && isPlayerBullet)
        {
            // Glont jucator loveste inamicul
            enemyHealth.TakeDamage((int)damageAmount); // Vom crea aceasta functie in EnemyController
            Destroy(gameObject); 
        }
    }
}