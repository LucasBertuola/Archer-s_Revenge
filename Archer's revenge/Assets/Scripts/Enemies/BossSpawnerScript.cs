using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerScript : MonoBehaviour
{

    public GameObject[] enemies = new GameObject[2];
    public GameObject spawnEffect;
    public int enemy;
    public float spawnTimer;
    public float startSpawnTimer;
    public float minDistanceY;
    public float maxDistanceY;
    public Transform Player;


    void Start()
    {
        spawnTimer = startSpawnTimer;
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Player != null)
        {
            if (Vector2.Distance(transform.position, Player.position) >= minDistanceY
                && Vector2.Distance(transform.position, Player.position) <= maxDistanceY &&
                spawnTimer <= 0 && GameManager.Instance.minotaur2ndFase == true)
            {
                enemy = Random.Range(0, enemies.Length);
                Instantiate(spawnEffect, transform.position, Quaternion.identity);
                Instantiate(enemies[enemy], transform.position, Quaternion.identity);
                spawnTimer = startSpawnTimer;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
}
