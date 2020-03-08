using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour {

    public float enemySpeed;
    public float shootingDistance;
    public float stoppingDistance;
    public float retreatDistance;
    //public float aggroDistance;
    //public float disengageDistance;
    public int health;
    private int lootChance;
    //public bool isAggroed;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject poisonSpit;
    public Transform player;
    public GameObject bloodEffect;
    public GameObject deathEffect;
    public GameObject healthPotion;
    private Animator animator;
    public AudioClip deathSound;
    public GameObject key;
    public AudioClip spitSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
        Physics2D.IgnoreLayerCollision(8, 10);
    }

    void Update()
    {
        //Aggro range
        /*if (Vector2.Distance(transform.position, player.position) < aggroDistance)
        {
            isAggroed = true;
        }
        else if (Vector2.Distance(transform.position, player.position) > disengageDistance)
        {
            isAggroed = false;
        }*/

        //Follow playerPos
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg + 90);

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance /*&& isAggroed*/)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance &&
            Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            animator.SetBool("isWalking", false);
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }



        //poison Spit
        if (timeBtwShots <= 0 && Vector2.Distance(transform.position, player.position) < shootingDistance)
        {
            AudioSource.PlayClipAtPoint(spitSound, transform.position);
            Instantiate(poisonSpit, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
        //death
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
    //damage
    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
    }
}
