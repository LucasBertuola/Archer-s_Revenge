using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehavior : StateMachineBehaviour {

    private Transform playerPos;
    public float timer;
    public float minTime;
    public float maxTime;
    public float speed;
    private int rand;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);

        if (timer <= 0)
        {
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animator.SetTrigger("attack1");
            }
            else
            {
                animator.SetTrigger("attack2");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}

}
