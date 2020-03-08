using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Behavior : StateMachineBehaviour {

    public float XPosMax;
    public float YPosMax;
    public float YPosMin;
    public float XPosMin;
    public float timer;
    public int rockQnt;
    private float randX;
    private float randY;

    private AudioSource source;
    public AudioClip moo;
    public GameObject rock;
    public Vector2 randPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        source.PlayOneShot(moo);

        timer = 2f;
        
        for (int i = 0; i < rockQnt; i++)
        {
            randPos.x = Random.Range(XPosMin, XPosMax);
            randPos.y = Random.Range(YPosMin, YPosMax);

            Instantiate(rock, randPos, Quaternion.identity);
        }
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            animator.SetTrigger("walk");
        }
    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }

}
