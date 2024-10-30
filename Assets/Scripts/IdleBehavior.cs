using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    private float timer;
    public float mintime;
    public float maxtime;
    private GameObject Player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(mintime, maxtime);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            timer -= Time.deltaTime;


            //Vector3 relativePos = Player.transform.position - animator.transform.position;
            //Quaternion rotation = Quaternion.LookRotation(relativePos);
            //rotation.x = animator.transform.rotation.x;
            //rotation.y = animator.transform.rotation.y;
            //animator.transform.rotation = rotation;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
