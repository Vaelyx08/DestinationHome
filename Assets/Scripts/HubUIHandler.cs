using UnityEngine;

public class HubUIHandler : MonoBehaviour
{
    public void ClickScavenge()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ScavengeForNextLevel();
        }
    }
    public void ClickHome()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AttemptToWin();
        }
    }
}