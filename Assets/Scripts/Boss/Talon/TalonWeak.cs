using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonWeak : StateMachineBehaviour
{
    public float damageReset = 12;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<TalonHealth>().damageTaken = 0;
        animator.GetComponent<TalonHealth>().StartTimer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<TalonHealth>().damageTaken >= damageReset)
        {
            animator.GetComponent<TalonHealth>().StopTimer();
            animator.SetTrigger("Regen");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
