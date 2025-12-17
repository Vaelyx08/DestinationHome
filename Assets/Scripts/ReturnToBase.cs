using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    public KeyCode retreatKey = KeyCode.F; 

        void Update()
        {
            // 1. Verificam daca tasta a fost apasata (doar o data)
            if (Input.GetKeyDown(retreatKey))
            {
                ExecuteRetreat();
            }
        }

        void ExecuteRetreat()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ReturnToBase();
            }
            else
            {
                Debug.LogError("GameManager nu a fost gasit! Nu se poate reveni la HUB.");
            }   
        }
}