using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

    public float timer;
	
	void Update () {
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
	}
}
