using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    public bool joystick;
    public Transform player;
    Vector3 last;
  

    void Start () {
        Cursor.visible = false;
    }

    void Update () {
        joystick = GameManager.Instance.controlindenfy;
        Debug.Log(joystick);
        if (joystick)
        {
            if(player != null)
            {
                GameManager.Instance.DisableAimMouse();
                Cursor.visible = false;
                transform.position = player.transform.position;

                if(Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal2") == 0)
                    transform.eulerAngles = last;
                else
                    last = new Vector3(0, 0, Mathf.Atan2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")) * 180 / Mathf.PI);

                transform.eulerAngles = last;
            }
         
        }
        else {
            GameManager.Instance.DisableAimJoystick();
            Cursor.visible = true;
            Vector2 cursorPoS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPoS;
        }
        
	}
}
