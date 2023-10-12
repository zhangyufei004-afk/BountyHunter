using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossSlam : StateMachineBehaviour
{
    public GameObject shockWavePrefab;
    public float shockSpeed = 15;
    private GameObject boss;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.gameObject;
        GameObject shockLeft = Instantiate(shockWavePrefab, new Vector3(boss.transform.position.x + 0.4f, boss.transform.position.y - .15f, 0), quaternion.identity);
        GameObject shockRight = Instantiate(shockWavePrefab, new Vector3(boss.transform.position.x - 0.4f, boss.transform.position.y - .15f, 0), quaternion.identity);
        shockLeft.GetComponent<ShockWave>().bulletSpeed = shockSpeed;
        shockRight.GetComponent<ShockWave>().bulletSpeed = shockSpeed;
        shockLeft.transform.Rotate(0, 180, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}