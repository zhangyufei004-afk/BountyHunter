using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private PlayerManager playerManager;
    public FloatReference bossDamage;
    public GameObject attackPoint;
    public float attackArea = 4f;
    public AudioSource attack;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer()
    {   
        if(Vector2.Distance(playerManager.player.transform.position, attackPoint.transform.position) < attackArea)
        {
            Debug.Log("Damaged Player");
            playerManager.player.GetComponent<PlayerHealth>().TakeDamage(bossDamage.Value);
        }
    }
    
    public void PlaySound()
    {
        attack.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackArea);
    }
}
