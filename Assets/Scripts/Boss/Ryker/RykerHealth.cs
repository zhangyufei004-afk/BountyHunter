using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RykerHealth : MonoBehaviour
{
    private Animator animator;
    public GameObject healthBar;
    public Image bar;
    private float currentHealth;
    [SerializeField] private float maxHealth = 15;
    public GameObject objective;
    public float spawnFreq = 5;
    private float timeUnitlSpawn = 0;


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

        if(timeUnitlSpawn >= spawnFreq)
        {
            animator.SetTrigger("SpawnTroops");
            timeUnitlSpawn = 0;
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
        currentHealth -= damage;
        timeUnitlSpawn += damage;
    }

    private void Die()
    {
        objective.SetActive(true);
        Destroy(gameObject);
    }
}
