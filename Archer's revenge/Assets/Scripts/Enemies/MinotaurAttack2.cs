using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttack2 : MonoBehaviour {

    private PlayerScript playerScript;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isInvincible == false && collision.gameObject.CompareTag("Player"))
        {
            playerScript.TakeDamage(2);
        }
    }
}
