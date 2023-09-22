using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Variables")]
    public float maxHealth;
    public float currentHealth;
    public GameObject coins;

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
        int count = UnityEngine.Random.Range(1, 4);
        for(int i = 0; i < count; i++)
        {
            Instantiate(coins, new Vector3(transform.position.x + UnityEngine.Random.Range(-1, 2), gameObject.transform.position.y, 0), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
