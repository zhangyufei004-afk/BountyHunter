using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerHealth>().health < 6)
            {
                other.gameObject.GetComponent<PlayerHealth>().ResetHealth();
                Destroy(gameObject);
            }
        }
    }
}
