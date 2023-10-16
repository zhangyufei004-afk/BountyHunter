using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonIdle : StateMachineBehaviour
{
    public GameObject bulletPrefab;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(GameObject spawn in animator.GetComponent<TalonHealth>().bulletSpawn)
        {
            Instantiate(bulletPrefab, spawn.transform.position, spawn.transform.rotation);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<BossStats>().isGrounded())
        {
            animator.SetBool("isGrounded", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Weakened");
    }
}
