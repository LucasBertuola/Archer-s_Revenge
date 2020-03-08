using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    //stats
    public int health;
    public float Speed;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int index;
    public float lerpTimer;

    //unity things
    private Rigidbody2D rb;
    private Vector2 moveVel;
    private Vector2 dashVel;
    public GameObject dashEffect;
    public GameObject dustEffect;
    public GameObject poisonEffect;
    public GameObject bloodEffect;
    public GameObject deathEffect;
    public GameObject healEffect;
    private Animator animator;
    public Slider HealthBar;
    private AudioSource source;
    public AudioClip[] damageSound;
    public AudioClip dashSound;
    public AudioClip drinkSound;
    public AudioClip grassSound;
    public AudioClip aggroSound;
    public AudioClip deathSound;
    public AudioClip keySound;
    public Image fill;
    public Color maxHealthColor;
    public Color minHealthColor;
    public Color healColor;
    public Color poisonColor;
    public Color invulnerableColor1;
    public Color invulnerableColor2;

    //triggers
    public bool isInvincible = false;
    public bool isPoisoned;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        Physics2D.IgnoreLayerCollision(10, 8);
        dashTime = startDashTime;
    }

    void Update()
    {
        if (health > 10)
        {
            health = 10;
        }

        //Movement
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //Flip
        Vector2 playerScale = transform.localScale;
        Vector2 cursorPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float worldXPos = Camera.main.ScreenToWorldPoint(cursorPos).x;

        if (worldXPos > gameObject.transform.position.x)
        {
            playerScale.x = 1;
            transform.localScale = playerScale;
        }
        else
        {
            playerScale.x = -1;
            transform.localScale = playerScale;
        }

        moveVel = moveInput.normalized * Speed;
        rb.MovePosition(rb.position + moveVel * Time.fixedDeltaTime);


        //Dash
        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }

        dashVel = moveInput.normalized * dashSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            dashTime = startDashTime;
            source.PlayOneShot(dashSound);
            rb.MovePosition(rb.position + dashVel * Time.fixedDeltaTime);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }

        //Health color
        if (lerpTimer > 0)
        {
            fill.color = Color.Lerp(maxHealthColor, healColor, Mathf.PingPong(Time.time, 1));
            lerpTimer -= Time.deltaTime;
        }
        else if (health <= 3)
        {
            fill.color = Color.Lerp(minHealthColor, maxHealthColor, Mathf.PingPong(Time.time, 1));

            if (isPoisoned)
            {
                fill.color = Color.Lerp(minHealthColor, poisonColor, Mathf.PingPong(Time.time, 1));
            }
        }
        else if (health > 3 && isPoisoned)
        {
            fill.color = Color.Lerp(maxHealthColor, poisonColor, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            fill.color = maxHealthColor;
        }


        //Death
        HealthBar.value = health;
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            source.PlayOneShot(deathSound);
            GameManager.Instance.IsDead();
            Destroy(gameObject);
        }
    }

    void ResetInvulnerability()
    {
        isInvincible = false;
    }

    public void DrinkPotion(int healthValue)
    {
        health += healthValue;
        Instantiate(healEffect, transform.position, Quaternion.identity, transform);
        source.PlayOneShot(drinkSound);
    }
    public void EatFlower(int healthValue)
    {
        health += healthValue;
        Instantiate(healEffect, transform.position, Quaternion.identity, transform);
        source.PlayOneShot(grassSound);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health--;
        isInvincible = true;
        StartCoroutine(DamageFade());
        Invoke("ResetInvulnerability", 2);
        index = Random.Range(0, damageSound.Length);
        source.PlayOneShot(damageSound[index]);
    }

    public void TakePoisonDamage(float poisonDelay, int poisonCount,int poisonDamage)
    {
        IsPoisoned(poisonDelay, poisonCount, poisonDamage);
        isInvincible = true;
        StartCoroutine(DamageFade());
        Invoke("ResetInvulnerability", 2);
        index = Random.Range(0, damageSound.Length);
        source.PlayOneShot(damageSound[index]);
    }

    public void IsPoisoned(float poisonDelay, int poisonCount, int poisonDamage)
    {
        StartCoroutine(DoPoisonDamage(poisonDelay, poisonCount, poisonDamage));
        Instantiate(poisonEffect, transform.position, Quaternion.identity, transform);
        isPoisoned = true;
    }

    IEnumerator DoPoisonDamage(float poisonDelay, int poisonCount, int poisonDamage)
    {
        int currentCount = 0;
        while (currentCount < poisonCount)
        {
            if (currentCount == poisonCount - 1)
            {
                isPoisoned = false;
            }
            health -= poisonDamage;
            yield return new WaitForSeconds(poisonDelay);
            currentCount++;
        }
    }
    IEnumerator DamageFade()
    {
        if (isInvincible)
        {
            animator.SetTrigger("TakeDamage");
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("potion"))
        {
            Destroy(collision.gameObject);
            lerpTimer = 2;
            DrinkPotion(1);
        }
        if (collision.CompareTag("flower"))
        {
            Destroy(collision.gameObject);
            lerpTimer = 2;
            EatFlower(3);
        }
        if (collision.CompareTag("caveTrigger"))
        {
            source.PlayOneShot(aggroSound);
            GameManager.Instance.EnterCave();
        }
        if (collision.CompareTag("tutorialTrigger"))
        {
            GameManager.Instance.EnterForest();
        }
        if (collision.CompareTag("key"))
        {
            source.PlayOneShot(keySound);
            GameManager.Instance.key = 1;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("gateTrigger"))
        {
            if (GameManager.Instance.key == 1)
            {
                GameManager.Instance.OpenGate();
                GameManager.Instance.key = 0;
            }

        }
    }
}
