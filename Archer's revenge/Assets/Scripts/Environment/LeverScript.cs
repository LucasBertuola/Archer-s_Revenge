using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    public GameObject leverOpen;
    public GameObject spikes;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bolt"))
        {
            leverOpen.SetActive(true);
            Destroy(spikes.gameObject);
            Destroy(gameObject);
        }
    }

    void RetractSpikes()
    {
        leverOpen.SetActive(true);
        Destroy(spikes.gameObject);
        Destroy(gameObject);
    }
}