using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttack : MonoBehaviour {
    public float timer;
    
	void Update () {
        Destroy(gameObject, timer);
    }
}
