using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    void Update()
    {
        GameObject target = playerManager.player;
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // if(angle > 90 || angle < -90)
        // {
        //     transform.localScale = new Vector3(1, -1, 1);
        // }
        // else
        // {
        //     transform.localScale = new Vector3(1, 1, 1);
        // }
    }
}
