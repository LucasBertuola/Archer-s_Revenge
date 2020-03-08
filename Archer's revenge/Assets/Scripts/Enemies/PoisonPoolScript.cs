using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPoolScript : MonoBehaviour {

    public int poisonDamage;

    private PlayerScript playerScript;

    void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
	
	void Update () {
        Destroy(gameObject, 5);


	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isInvincible == false && collision.gameObject.tag == "Player")
        {
            playerScript.TakePoisonDamage(5, 2, poisonDamage);
        }
    }
}
