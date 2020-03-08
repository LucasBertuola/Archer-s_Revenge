using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {

    public float boltSpeed;
    public float distance;
    public float lifeTime;
    public int damage;

    public LayerMask whatIsSolid;
    public GameObject boltEffect;
    public AudioClip impactSound;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("enemy"))
            {
                hitInfo.collider.GetComponent<EnemyScript>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("enemy2"))
            {
                hitInfo.collider.GetComponent<Enemy2Script>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("bossMinotaur"))
            {
                hitInfo.collider.GetComponent<MinotaurScript>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("lever"))
            {
                GameManager.Instance.RetractSpikes();
            }
            DestroyProjectile();
        }

        transform.Translate(Vector2.right * boltSpeed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        AudioSource.PlayClipAtPoint(impactSound, transform.position);
        Instantiate(boltEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
