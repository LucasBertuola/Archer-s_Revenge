using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowScript : MonoBehaviour {

    private GameManager gameManager;

    public float offset;
    public GameObject bolt;
    public AudioClip shootingSoung;
    private AudioSource source;
    public Transform boltSpawn;
    public float boltCD = 0;
    public float fireDelay = 0.5f;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update () {
        Vector2 crossbowScale = transform.localScale;
        Vector2 cursorPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float worldXPos = Camera.main.ScreenToWorldPoint(cursorPos).x;

        if (worldXPos > gameObject.transform.position.x)
        {
            crossbowScale.x = 1;
            crossbowScale.y = 1;
            transform.localScale = crossbowScale;
        }
        else
        {
            crossbowScale.x = -1;
            crossbowScale.y = -1;
            transform.localScale = crossbowScale;
        }


        boltCD -= Time.deltaTime;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetMouseButton(0) && boltCD <= 0f)
        {
            Instantiate(bolt, boltSpawn.position, transform.rotation);
            source.PlayOneShot(shootingSoung);
            boltCD = fireDelay;
        }
	}
}
