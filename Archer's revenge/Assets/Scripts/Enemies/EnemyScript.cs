using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float speed;
    public int health = 2;
    public float aggroDistance;
    public float disengageDistance;
    public bool isAggroed;
    //private float waitTime;
    public float startWaitTime;
    private int lootChance;

    /*public float minX;
    public float maxX;
    public float minY;
    public float maxY;*/

    private Transform playerPos;
    private PlayerScript playerScript;
    public GameObject deathEffect;
    public GameObject bloodEffect;
    public GameObject healthPotion;
    private Animator animator;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public GameObject key;
    //public Transform moveSpot;

    void Start ()
    {
        //waitTime = startWaitTime;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

	void Update ()
    {
        if (playerScript != null)
        {
            //Aggro range
            if (Vector2.Distance(transform.position, playerPos.position) < aggroDistance)
            {
                isAggroed = true;
            }
            else if (Vector2.Distance(transform.position, playerPos.position) > disengageDistance)
            {
                isAggroed = false;
            }

            Vector2 enemyScale = transform.localScale;

            if (playerPos.position.x > gameObject.transform.position.x)
            {
                enemyScale.x = 1;
                transform.localScale = enemyScale;
            }
            else
            {
                enemyScale.x = -1;
                transform.localScale = enemyScale;
            }

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            if (isAggroed)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }



            if (health <= 0)
            {
                lootChance = Random.Range(0, 100);
                GameManager.Instance.enemyKillCount -= 1;
                GameManager.Instance.enemyCount -= 1;
                if (lootChance >= 90)
                {
                    Instantiate(healthPotion, transform.position, Quaternion.identity);
                }
                if (GameManager.Instance.enemyKillCount == 0)
                {
                    Instantiate(key, transform.position, Quaternion.identity);
                }
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isInvincible == false && collision.gameObject.CompareTag("Player"))
        {
            playerScript.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 1)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
    }
}
