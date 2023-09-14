using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    public GameObject[] bulletUI;

    private int ammo = 8;
    public int maxAmmo = 8;

    private bool isReloading;

    public InputActionReference shootReference;
    public InputActionReference reloadReference;

    void OnEnable()
    {
        shootReference.action.performed += ShootBullet;
        reloadReference.action.performed += ReloadRef;
    }
    
    void OnDisable()
    {
        shootReference.action.performed -= ShootBullet;
        reloadReference.action.performed -= ReloadRef;
    }

    void Start()
    {
        isReloading = false;
        ammo = maxAmmo;
        for(int i = 0; i < bulletUI.Length; i++)
        {
            bulletUI[i].SetActive(false);
        }
        bulletUI[ammo].SetActive(true);
    }

    private void ShootBullet(InputAction.CallbackContext context)
    {
        if(ammo > 0 && !isReloading)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletUI[ammo].SetActive(false);
            ammo--;
            bulletUI[ammo].SetActive(true);
        }
        else
        {
            Reload();
        }
    }

    private void ReloadRef(InputAction.CallbackContext context)
    {
        Reload();
    }

    private void Reload()
    {
        if(!isReloading)
            StartCoroutine(ReloadTime());
    }

    IEnumerator ReloadTime()
    {
        isReloading = true;
        
        while(ammo < maxAmmo)
        {
            bulletUI[ammo].SetActive(false);
            ammo++;
            bulletUI[ammo].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        isReloading = false;
    }
}
