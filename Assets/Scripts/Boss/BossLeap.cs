using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeap : StateMachineBehaviour
{
   public float jumpHeight = 5;

   private Rigidbody2D rb;
   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      rb = animator.GetComponent<Rigidbody2D>();
      rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
      animator.SetBool("isGrounded", false);
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      animator.SetBool("isGrounded", animator.GetComponent<BossStats>().isGrounded());
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      animator.ResetTrigger("Leap");
   }
}
