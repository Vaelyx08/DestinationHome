using UnityEngine;

public class HubConsole : MonoBehaviour
{
    public float interactionRange = 3f; 
    public KeyCode interactionKey = KeyCode.E; 
    public GameObject courseMenuPanel;

    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.LogError("HubConsole nu gaseste GameManager!");
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= interactionRange)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                ShowCourseMenu();
            }
        }
    }
    
  public void ShowCourseMenu()
    {
        if (courseMenuPanel != null)
        {
            courseMenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Consola interactiva deschisa. Alege Cursul.");
        }
        else
        {
            Debug.LogError("Referinta courseMenuPanel lipseste din HubConsole!");
        }
    }

    public void HideCourseMenu()
    {
     //   if (courseMenuPanel != null)
        {
            courseMenuPanel.SetActive(false);
        }
    }
}