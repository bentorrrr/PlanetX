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

            // Can you help me fix this to rotate follow the player 360 degrees? right now it only follows the player only around 180 degress and then it flips backwardsVector3 
            Vector3 relativePos = Player.transform.position - animator.transform.position;
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            animator.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
