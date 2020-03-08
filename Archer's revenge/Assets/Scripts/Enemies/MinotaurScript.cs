using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinotaurScript : MonoBehaviour {

    public int health;

    private PlayerScript playerScript;
    public GameObject bloodEffect;
    private Animator anim;
    public Slider HealthBar;
    public GameObject deathEffect;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    void Update()
    {
        HealthBar.value = health;

        if (health == 50)
        {
            GameManager.Instance.minotaur2ndFase = true;
        }

        if (health <= 0)
        {
            anim.SetTrigger("die");
            Invoke("IsDying", 3);
        }
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerScript.isInvincible == false && collision.gameObject.CompareTag("Player"))
        {
            playerScript.TakeDamage(2);
        }
    }

    public void IsDying()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameManager.Instance.Won();
        playerScript.isInvincible = true;
        playerScript.StopAllCoroutines();
        Destroy(gameObject);
    }
}