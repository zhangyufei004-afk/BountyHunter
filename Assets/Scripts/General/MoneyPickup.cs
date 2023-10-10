using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public Money money;
    public AudioSource moneySound;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            moneySound.Play();
            Destroy(gameObject);
            money.IncrementMoney();
        }
    }
}
