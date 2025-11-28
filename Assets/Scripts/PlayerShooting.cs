using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;

    public int maxAmmoInGun = 6;
    public int currentAmmoInGun = 6;
    public int ammoReserve = 0;

    public KeyCode reloadKey = KeyCode.R;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click stanga
        {
            Shoot();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmoInGun <= 0)
        {
            Debug.Log("No ammo, reload using R.");
            return;
        }

        currentAmmoInGun = currentAmmoInGun - 1;

        // Aici instantiem glontul ca in varianta ta originala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 2f); // se sterge automat dupa 2 sec

        Debug.Log("Bang! Ammo loaded: " + currentAmmoInGun);
    }

    public void AddAmmo(int amount)
    {
        ammoReserve = ammoReserve + amount;
    }

    void Reload()
    {
        if (ammoReserve <= 0)
        {
            Debug.Log("No ammo in reserve!");
            return;
        }

        currentAmmoInGun = 0; // Pierdem ce aveam incarcat
        int loadAmount = Mathf.Min(maxAmmoInGun, ammoReserve);
        currentAmmoInGun = loadAmount;
        ammoReserve = ammoReserve - loadAmount;
        Debug.Log("Reloaded. Ammo in gun: " + currentAmmoInGun);
    }
}
