using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Attachments")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Variables")]
    private int ammo = 8;
    public int maxAmmo = 8;
    public float reloadSpeed = 0.5f;

    private bool isReloading;

    void Start()
    {
        isReloading = false;
        ammo = maxAmmo;
    }

    public void ShootBullet()
    {
        if(ammo > 0 && !isReloading)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Reload();
        }
    }

    private void Reload()
    {
        if(!isReloading)
            StartCoroutine(ReloadTime());
    }

    IEnumerator ReloadTime()
    {
        isReloading = true;     
        yield return new WaitForSeconds(reloadSpeed);
        ammo = maxAmmo;
        isReloading = false;
    }
}