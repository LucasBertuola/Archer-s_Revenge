using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpitScript : MonoBehaviour {

    public float distance;
    public float speed;
    public int poisonDamage;
    public LayerMask whatIsSolid;

    public GameObject poisonExplosionEffect;
    public GameObject poisonPool;
    private Transform playerPos;
    private PlayerScript playerScript;
    private Vector2 target;

	void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();


        target = new Vector2(playerPos.position.x, playerPos.position.y);
	}
	
	void Update () {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.tag == "Player" && playerScript.isInvincible == false)
            {
                playerScript.TakePoisonDamage(5, 2, poisonDamage);
            }
            DestroyProjectile();
        }

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Instantiate(poisonPool, transform.position, transform.rotation);
            DestroyProjectile();
        }

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(poisonExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
