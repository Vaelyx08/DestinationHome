using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
<<<<<<< HEAD
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    private Door lastLookedDoor; // Re?inem u?a la care ne-am uitat anterior

    void Update()
    {
        // 1. Verificãm PRIVIREA (pentru culoare/UI)
        CheckLookAt();

        // 2. Verificãm INTERAC?IUNEA (pentru deschidere)
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void CheckLookAt()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Door door = hit.collider.GetComponent<Door>() ?? hit.collider.GetComponentInParent<Door>();

            if (door != null)
            {
                if (lastLookedDoor != door)
                {
                    if (lastLookedDoor != null) lastLookedDoor.SetHighlight(false);
                    door.SetHighlight(true);
                    lastLookedDoor = door;
                }
            }
            else
            {
                ClearLastDoor();
            }
        }
        else
        {
            ClearLastDoor();
        }
    }

    void ClearLastDoor()
    {
        if (lastLookedDoor != null)
        {
            lastLookedDoor.SetHighlight(false);
            lastLookedDoor = null;
        }
    }

    void TryInteract()
    {
        // Deoarece CheckLookAt deja ?tie la ce u?ã ne uitãm, folosim direct variabila
        if (lastLookedDoor != null)
        {
            lastLookedDoor.ToggleDoor();
        }
    }
=======

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

>>>>>>> 4ad08d4a558a971baa0a9a713443b9377c32e9af
}