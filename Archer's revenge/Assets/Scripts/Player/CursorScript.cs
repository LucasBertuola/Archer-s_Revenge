using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

	void Start () {
        Cursor.visible = false;
	}
	
	void Update () {
        Vector2 cursorPoS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPoS;
	}
}
