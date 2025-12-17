using UnityEngine;

public class Door : MonoBehaviour
{

    public bool isOpen = false;
    public float openAngle=90f;
    public float openSpeed=4f;
    private UnityEngine.AI.NavMeshObstacle obstacle;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation=transform.rotation;
        openRotation=Quaternion.Euler(transform.eulerAngles + new Vector3(0,openAngle, 0));
        if (obstacle != null) obstacle.carving = true;
    }

    public void ToggleDoor()
    {
        isOpen=!isOpen;
        if (obstacle != null) obstacle.enabled =!obstacle.enabled;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation)>0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

}
