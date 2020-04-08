using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteScene : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    private void Awake()
    {
        player.GetComponent<PlayerScript>().enabled = false;
    }
    void Update()
    {
        player.transform.position = transform.position;
    }

    public void EnablePlayer()
    {
        player.GetComponent<PlayerScript>().enabled = true;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
