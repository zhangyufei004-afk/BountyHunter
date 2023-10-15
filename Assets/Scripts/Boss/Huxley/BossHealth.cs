using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Animator animator;
    public GameObject healthBar;
    public Image bar;
    private float currentHealth;
    [SerializeField] private float maxHealth = 15;
    [SerializeField] private int leapFreq = 3;
    private float timeUntilLeap = 0;
    public GameObject objective;


    void Start()
    {
        objective.SetActive(false);
        healthBar.SetActive(false);
        currentHealth = maxHealth;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(animator.GetBool("PlayerEnteredArena"))
        {
            healthBar.SetActive(true);
            SetHealthBar();
        }

        if(currentHealth <= 0)
        {
            Die();
        }

        if(timeUntilLeap >= leapFreq)
        {
            animator.SetTrigger("Leap");
            animator.SetBool("Leaping", true);
            StartCoroutine(LeapWait());
            timeUntilLeap = 0;
        }
    }

    IEnumerator LeapWait()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Leaping", false);
    }

    private void SetHealthBar()
    {
        bar.fillAmount = 1 / maxHealth * currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        timeUntilLeap += damage;
    }

    private void Die()
    {
        objective.SetActive(true);
        Destroy(gameObject);
    }
}