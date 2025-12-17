using UnityEngine;
using UnityEngine.UI;

public class MenuButtonLinker : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(HandleClick);
        }
        else
        {
            Debug.LogError("MenuButtonLinker: Nu am găsit componenta Button pe " + gameObject.name);
        }
    }

    void HandleClick()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("MenuButtonLinker: Trimitere comandă StartNewGame către GameManager.");
            GameManager.Instance.StartNewGame();
        }
        else
        {
            Debug.LogError("MenuButtonLinker: GameManager.Instance este NULL!");
        }
    }
}