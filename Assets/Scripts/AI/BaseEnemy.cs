using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Variables")]
    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        for(int i = 0; i < damage; i++)
        {
            currentHealth--;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
