using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;
    public FloatReference enemyDamage;

    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("hit " + other.name); 

        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage.Value);
        }
        
        if(other.gameObject.tag == "Ground")
        {
            return;
        }
        Destroy(gameObject);
    }
}
