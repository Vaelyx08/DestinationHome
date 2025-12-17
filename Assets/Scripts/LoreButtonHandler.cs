using UnityEngine;

public class LoreButtonHandler : MonoBehaviour
{
    public void OnContinueClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartLevel1FromLore();
        }
        else
        {
            Debug.LogError("GameManager.Instance nu a fost gasit! Asigura-te ca GameManager este incarcat inainte de LoreScreen.");
        }
    }
}