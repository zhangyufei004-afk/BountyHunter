using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalonFloating : StateMachineBehaviour
{
    public GameObject[] enemySpawn;
    public GameObject brawler;
    public int enemiesKilled = 0;
    public float height = 10;
    private GameObject talon;
    public float speed = 10;
    private Vector2 target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().gravityScale = 0;
        talon = animator.gameObject;
        target = new Vector3(talon.transform.position.x, talon.transform.position.y + height, 0);
        animator.SetBool("isGrounded", false);
        animator.GetComponent<TalonHealth>().isInvulnerable = true;
        animator.GetComponent<BossStats>().isFlying = true;
        enemySpawn = animator.gameObject.GetComponent<TalonHealth>().enemySpawnPoints;
        foreach (GameObject spawnpoint in enemySpawn)
        {
            Instantiate(brawler, spawnpoint.transform);
            enemiesKilled++;
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(target, talon.transform.position) >= 0.1)
        {
            talon.transform.position = Vector2.MoveTowards(talon.transform.position, target, speed * Time.deltaTime);
        }

        if(enemiesKilled <= 0)
        {
            animator.SetTrigger("Weakened");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().gravityScale = 4;
        animator.GetComponent<TalonHealth>().isInvulnerable = false;
        animator.GetComponent<BossStats>().isFlying = false;
        animator.ResetTrigger("Regen");
    }
}
