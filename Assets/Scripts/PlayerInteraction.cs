using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Door door = hit.collider.GetComponent<Door>() 
                     ?? hit.collider.GetComponentInParent<Door>();

            if (door != null)
            {
                door.ToggleDoor();
            }
        }

        // DEBUG vizual (foarte important)
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.yellow, 1f);
    }
}
