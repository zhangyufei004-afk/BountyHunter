using System;
using System.Collections;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Variables")]
    public float maxHealth;
    public float currentHealth;
    public GameObject coins;
    public AudioSource hitSound;
    private SpriteRenderer spriteRenderer;
    private bool isBossEnemy = false;
    private bool isTalonEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        isBossEnemy = gameObject.GetComponent<EnemyBehavior>().isBossBattle;
        isTalonEnemy = gameObject.GetComponent<EnemyBehavior>().isTalonEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(FlashRed());
        hitSound.Play();
        for(int i = 0; i < damage; i++)
        {
            currentHealth--;
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        if(isBossEnemy && isTalonEnemy)
        {
            GameObject.FindWithTag("Talon").GetComponent<Animator>().GetBehaviour<TalonFloating>().enemiesKilled -= 1;
        }
        int count = UnityEngine.Random.Range(1, 4);
        for(int i = 0; i < count; i++)
        {
            Instantiate(coins, new Vector3(transform.position.x + UnityEngine.Random.Range(-1, 2), gameObject.transform.position.y, 0), Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void DieWithoutCoins()
    {
        if(isBossEnemy && isTalonEnemy)
        {
            GameObject.FindWithTag("Talon").GetComponent<Animator>().GetBehaviour<TalonFloating>().enemiesKilled -= 1;
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            DieWithoutCoins();
        }
    }
}
