using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 4f;

    private bool blockedByPlayer = false;

    private NavMeshObstacle obstacle;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Awake()
    {
        obstacle = GetComponent<NavMeshObstacle>();
    }

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * openAngle);

        if (obstacle != null)
        {
            obstacle.carving = true;
            obstacle.enabled = true; // închis
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen));
    }

    private IEnumerator RotateDoor(bool open)
    {
        if (obstacle != null)
            obstacle.enabled = !open; // blocãm AI doar când e închisã

        Quaternion targetRotation = open ? openRotation : closedRotation;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            if (blockedByPlayer)
                yield break;
        
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            blockedByPlayer = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            blockedByPlayer = false;
    }

}
