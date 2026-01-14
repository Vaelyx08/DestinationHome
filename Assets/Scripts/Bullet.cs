using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageAmount = 1f;
<<<<<<< HEAD
    public bool isPlayerBullet = false;

    void OnTriggerEnter(Collider other)
    {
        // 1. Ignorãm coliziunea cu cel care a tras
        if (other.CompareTag("Player") && isPlayerBullet) return;
        if (other.CompareTag("Enemy") && !isPlayerBullet) return;

        // 2. Verificãm dacã am lovit un Player
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null && !isPlayerBullet)
        {
            playerHealth.TakeDamage((int)damageAmount);
            Destroy(gameObject);
            return; // Oprim func?ia aici
        }

        // 3. Verificãm dacã am lovit un Inamic
        Enemy_TOL_Chase enemyHealth = other.GetComponent<Enemy_TOL_Chase>();
        if (enemyHealth != null && isPlayerBullet)
        {
            enemyHealth.TakeDamage((int)damageAmount);
            Destroy(gameObject);
            return; // Oprim func?ia aici
        }

        // 4. SOLU?IA PENTRU U?Ã/PERE?I:
        // Dacã glon?ul love?te orice are un Collider ?i nu este un "Trigger" (ca o zonã invizibilã)
        if (!other.isTrigger)
        {
            // Aici po?i adãuga un efect de particule (spark) dacã vrei
            Debug.Log("Glon?ul a lovit: " + other.name);
            Destroy(gameObject);
=======
    public bool isPlayerBullet = false; // Flag pentru a sti cine a tras
    
    // Asigura-te ca GameObject-ul glontului are un Collider setat pe IsTrigger = true
    void OnTriggerEnter(Collider other)
    {
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
>>>>>>> 4ad08d4a558a971baa0a9a713443b9377c32e9af
        }
    }
}