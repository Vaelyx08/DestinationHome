using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenHandler : MonoBehaviour
{
    public void GoToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame(); 
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene("menu");
    }
}