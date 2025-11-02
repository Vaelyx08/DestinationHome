using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [SerializeField] float smoothSpeed = 5f; // cat de repede prinde din urma playerul

    void LateUpdate()
    {
        if (target == null) return;

        // Pozitia dorita (unde ar trebui sa fie camera)
        Vector3 desiredPosition = target.position + offset;

        // Pozitia actuala devine o interpolare intre cea curenta si cea dorita
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
