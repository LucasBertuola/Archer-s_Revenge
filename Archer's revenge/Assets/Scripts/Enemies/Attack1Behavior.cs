using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Behavior : StateMachineBehaviour {

    public float timer;
    private int rand;

    private AudioSource source;
    public AudioClip moo;
    public Vector2 Pos;
    public GameObject spikes;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        source.PlayOneShot(moo);

        timer = 4.5f;

        Pos.x = 9.5f;
        Pos.y = 78.5f;

        Instantiate(spikes, Pos, Quaternion.identity);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            animator.SetTrigger("walk");
        }
    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
