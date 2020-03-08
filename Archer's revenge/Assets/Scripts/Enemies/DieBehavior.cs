using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehavior : StateMachineBehaviour {

    public AudioClip deathSound;
    private AudioSource source;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        source.PlayOneShot(deathSound);
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
}
