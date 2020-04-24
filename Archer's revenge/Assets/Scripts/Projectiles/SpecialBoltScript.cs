using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBoltScript : BoltScript
{
    public int countDamageLife = 3;
    GameObject hited;
    public override void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject != hited)
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

                if (countDamageLife <= 0)
                    DestroyProjectile();

                countDamageLife--;

                hited = hitInfo.collider.gameObject;
            }

         
        }

        transform.Translate(Vector2.right * boltSpeed * Time.deltaTime);
    }
}
