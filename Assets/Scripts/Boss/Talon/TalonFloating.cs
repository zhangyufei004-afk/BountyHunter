using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonFloating : StateMachineBehaviour
{
    public GameObject[] enemySpawn;
    public GameObject brawler;
    public int enemiesKilled = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
        if(enemiesKilled <= 0)
        {
            animator.SetTrigger("Weakened");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<TalonHealth>().isInvulnerable = false;
        animator.GetComponent<BossStats>().isFlying = false;
        animator.ResetTrigger("Regen");
    }
}
