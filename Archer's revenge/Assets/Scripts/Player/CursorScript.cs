using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    public bool joystick = true;
    public Transform player;
    float rotat;
  

    void Start () {
        Cursor.visible = false;
	}
	
	void Update () {
        if (joystick)
        {
            transform.position = player.transform.position;
            rotat = Input.GetAxisRaw("Horizontal2");
    
            if (rotat != 0)
            {
                transform.Rotate(new Vector3(0, 0, rotat * -5));
            }
        }
        else { 
            Vector2 cursorPoS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPoS;
        }
        
	}
}
