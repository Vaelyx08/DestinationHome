using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {
        if(Input.GetKeyDown(interactKey))
        {
           InteractWithDoors();
        }
    }

    void InteractWithDoors() {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactDistance);

        foreach (Collider hit in hits)
        {
            Door door = hit.GetComponent<Door>();
            if (door == null)
                door = hit.GetComponentInParent<Door>();
            if (door != null)
            {
                door.ToggleDoor();
                break;
            }
        }
    }

}