using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth=maxHealth;
        hud = FindObjectOfType<HUD>();
        hud.UpdateHP(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        
        if (hud != null)
        {
             hud.UpdateHP(currentHealth);
        }

        if (currentHealth <= 0) 
        {
            PlayerDied(); 
        }
    }

    void PlayerDied()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied(); // Incarca DeathScreen
        }
        else
        {
             // Fallback in caz ca GameManager nu e in scena (util pentru testare)
             Debug.Log("GameManager nu a fost gasit. Jucatorul a murit. Incarca scena curenta (Test)");
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Heal()
    {
        currentHealth = maxHealth;
        hud.UpdateHP(currentHealth);
    }

    private HUD hud;
}
