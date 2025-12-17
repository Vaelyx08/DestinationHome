using UnityEngine;

public class ProvisionsPickup : MonoBehaviour
{
    public float rotationSpeed = 50f;

        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }

    private void Collect()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectProvision();
            Debug.Log("Provisions collected!");
        }
        else
        {
            Debug.LogError("GameManager nu a fost gasit in scena!");
        }
        Destroy(gameObject);
    }   
}