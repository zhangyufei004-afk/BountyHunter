using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalonHealth : MonoBehaviour
{
    private Animator animator;
    public GameObject healthBar;
    public Image bar;
    private float currentHealth;
    [SerializeField] private float maxHealth = 15;
    public GameObject objective;
    public bool isInvulnerable = false;
    public GameObject[] enemySpawnPoints;
    public float downTime = 12;


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
    }

    private void SetHealthBar()
    {
        bar.fillAmount = 1 / maxHealth * currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if(!isInvulnerable)
        {
            currentHealth -= damage;
        }
    }

    private void Die()
    {
        objective.SetActive(true);
        Destroy(gameObject);
    }

    public void StartTimer()
    {
        StartCoroutine(TimeDowned());
    }

    public IEnumerator TimeDowned()
    {
        yield return new WaitForSeconds(downTime);
        animator.SetTrigger("Regen");
    }
}
