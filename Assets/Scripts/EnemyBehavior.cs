using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private PlayerManager playerManager;

    public float speed = 5;

    public float playerMoveDistance = 5f;
    public float aIStopDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerManager.player.transform.position, transform.position);

        if(distance <= playerMoveDistance && distance > aIStopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerManager.player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerMoveDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aIStopDistance);
    }
}
