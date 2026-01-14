using UnityEngine;
using TMPro;

public class InteractUIController : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;
    public LayerMask doorLayer;
    public TextMeshProUGUI interactText;

    void Update()
    {
        //CheckDoor();
        interactText.enabled = true;
    }

    void CheckDoor()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, doorLayer))
        {
            if (hit.collider.GetComponentInParent<Door>())
            {
                interactText.enabled = true;
                return;
            }
        }

        interactText.enabled = false;
    }
}
