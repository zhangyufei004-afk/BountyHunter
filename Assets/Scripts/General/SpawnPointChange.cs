using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointChange : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.spawnPoint = gameObject.transform;
        }
    }
}
