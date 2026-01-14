using UnityEngine;
<<<<<<< HEAD
using System.Collections;

public class Door : MonoBehaviour
{
    [Header("Referinte")]
    public GameObject uiPanel; 
    public Rigidbody rb;

    [Header("Setari Usa")]
    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 4f;

    [Header("Efect Vizual")]
    public Color highlightColor = Color.yellow; 

    private bool isNearDoor = false;
    private Vector3 closedEuler;
    private Vector3 openEuler;
    private Color originalColor;
    private Renderer doorRenderer;
    private Coroutine currentCoroutine;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            closedEuler = rb.transform.eulerAngles;
            openEuler = closedEuler + new Vector3(0, openAngle, 0);
        }
    }

    void Start()
    {
        if (uiPanel == null) uiPanel = GameObject.FindWithTag("DoorUI");
        if (uiPanel != null) uiPanel.SetActive(false);

        doorRenderer = GetComponentInChildren<Renderer>();
        if(doorRenderer != null) originalColor = doorRenderer.material.color;
    }

    // --- SOLU?IA PENTRU E INSTANTANEU ---
    void Update()
    {
        // Verificãm E în Update, dar DOAR dacã isNearDoor e true
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Cât timp e?ti înãuntru, for?ãm isNearDoor sã fie true
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            
            // Dacã din gre?ealã UI-ul sau culoarea s-au oprit, le pornim la loc
            if (uiPanel != null && !uiPanel.activeSelf) uiPanel.SetActive(true);
            if (doorRenderer != null && doorRenderer.material.color != highlightColor) 
                doorRenderer.material.color = highlightColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            if (uiPanel != null) uiPanel.SetActive(false);
            if (doorRenderer != null) doorRenderer.material.color = originalColor;
        }
=======

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
>>>>>>> 4ad08d4a558a971baa0a9a713443b9377c32e9af
    }

    public void ToggleDoor()
    {
<<<<<<< HEAD
        if (rb == null) return;
        isOpen = !isOpen;
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(RotateDoor(isOpen));
    }

    private IEnumerator RotateDoor(bool open)
    {
        Quaternion targetRotation = Quaternion.Euler(open ? openEuler : closedEuler);
        while (Quaternion.Angle(rb.rotation, targetRotation) > 0.1f)
        {
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, openSpeed * Time.deltaTime * 50f));
            yield return null;
        }
        rb.MoveRotation(targetRotation);
    }

    public void SetHighlight(bool state)
    {
        // Aprindem/stingem UI-ul
        if (uiPanel != null) uiPanel.SetActive(state);

        // Schimbãm culoarea
        if (doorRenderer != null)
        {
            doorRenderer.material.color = state ? highlightColor : originalColor;
        }
    }
}
=======
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
>>>>>>> 4ad08d4a558a971baa0a9a713443b9377c32e9af
