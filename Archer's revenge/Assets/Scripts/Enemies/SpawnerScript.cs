using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject[] enemies = new GameObject[2];
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

    void Update () {
        if (Vector2.Distance(transform.position, Player.position) >= minDistanceY
            && Vector2.Distance(transform.position, Player.position) <= maxDistanceY &&
            spawnTimer <= 0 && GameManager.Instance.enemyCount < 20)
        {
            enemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemy], transform.position, Quaternion.identity);
            spawnTimer = startSpawnTimer;
            GameManager.Instance.enemyCount += 1;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
