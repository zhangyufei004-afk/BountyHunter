using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float fallSpeed = 2;

    void Update()
    {
        if(!isGrounded())
        {
            // Fall();
        }
    }
    
    public bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheck.localScale, 0, groundLayer);
    }

    public void Fall()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -fallSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheck.localScale);
    }
}
