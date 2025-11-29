using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ammoText;

    public void UpdateHP(int currentHP)
    {
        hpText.text = "HP: " + currentHP;
    }

    public void UpdateAmmo(int ammoInGun, int ammoReserve)
    {
        ammoText.text = "Ammo: " + ammoInGun + " / " + ammoReserve;
    }
}
