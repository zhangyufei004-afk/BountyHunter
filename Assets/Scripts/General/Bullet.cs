using UnityEngine;

public class Bullet : MonoBehaviour
{
    public FloatReference bulletSpeed;
    public Rigidbody2D rb;
    public FloatReference playerDamage;
    public FloatReference enemyDamage;

    void Start()
    {
        rb.velocity = transform.right * bulletSpeed.Value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<BaseEnemy>().TakeDamage(playerDamage.Value);
        }
        
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage.Value);
        }

        if(other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<BossHealth>().TakeDamage(playerDamage.Value);
        }
        
        if(other.gameObject.tag == "Ryker")
        {
            other.gameObject.GetComponent<RykerHealth>().TakeDamage(playerDamage.Value);
        }
        
        if(other.gameObject.tag == "Talon")
        {
            other.gameObject.GetComponent<TalonHealth>().TakeDamage(playerDamage.Value);
        }

        Destroy(gameObject);
    }
}
