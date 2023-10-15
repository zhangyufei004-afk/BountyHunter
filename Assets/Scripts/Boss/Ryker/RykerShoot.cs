using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RykerShoot : MonoBehaviour
{
    [Header("Attachments")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    public GameObject[] enemySpawnPoints;
    public GameObject[] teleportLocations;
    private Animator animator;
    // [SerializeField] private AudioSource shootSound;

    [Header("Variables")]
    public int ammo = 8;
    public int maxAmmo = 8;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        ammo = maxAmmo;
    }

    public void ShootBullet()
    {
        if(ammo > 0)
        {
            // shootSound.Play();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            ammo--;
        }
        else
        {
            Reload();
        }
    }

    private void Reload()
    {
        animator.SetTrigger("Teleport");
        ammo = maxAmmo;
    }

    public void ShootBulletTrigger(float timeToWait)
    {
        StartCoroutine(ShootBullet(timeToWait));
    }

    IEnumerator ShootBullet(float timeToWait)
    {
        if(ammo >= 0)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(timeToWait);
            StartCoroutine(ShootBullet(timeToWait));
        }
    }

}
