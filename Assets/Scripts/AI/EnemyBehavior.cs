using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private PlayerManager playerManager;

    [Header("Variables")]
    public float speed = 5;
    public float playerMoveDistance = 5f;
    public float aIStopDistance = 1.5f;
    public float attackDistance = 1.5f;
    public float attackSpeed = 2f;
    public bool isLongRange = false;
    public FloatReference enemyDamage;

    [Header("Attachments")]
    public GameObject[] points;
    public Transform activePoint;
    private int count = 0;

    private bool justDeltDamage = false; 

    // Start is called before the first frame update
    void Start()
    {
        activePoint = points[count].transform;
        playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector2.Distance(playerManager.player.transform.position, transform.position);
        // float pointDistance = Vector2.Distance(activePoint.position, transform.position);

        if(playerDistance <= playerMoveDistance && playerDistance > aIStopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerManager.player.transform.position.x, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            if(playerDistance > aIStopDistance)
                transform.position = Vector2.MoveTowards(transform.position, activePoint.position, speed * Time.deltaTime);
            
        }
        
        if(Vector2.Distance(activePoint.position, transform.position) <= 0)
        {
            ChangeActivePoint();
        }

        if(playerDistance <= attackDistance && !justDeltDamage && !isLongRange)
        {
            StartCoroutine(DealDamageMelee());
        }
        
        if(playerDistance <= attackDistance && !justDeltDamage && isLongRange)
        {
            
            StartCoroutine(DealDamageRanged());
        }
    }

    private void ChangeActivePoint()
    {
        if(count + 1 <= points.Length - 1)
        {
            count++;
            activePoint = points[count].transform;
        }
        else
        {
            count = 0;
            activePoint = points[count].transform;
        }
    }

    IEnumerator DealDamageMelee()
    {
        justDeltDamage = true;
        playerManager.player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage.Value);
        yield return new WaitForSeconds(attackSpeed);
        justDeltDamage = false;
    }

    IEnumerator DealDamageRanged()
    {
        justDeltDamage = true;
        gameObject.GetComponent<EnemyShoot>().ShootBullet();
        yield return new WaitForSeconds(attackSpeed);
        justDeltDamage = false;
    }

    // Try stop gliching when hitting wall and chasing player
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if(other.gameObject.tag == "EnemyWall")
    //     {
            
    //     }
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerMoveDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aIStopDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
