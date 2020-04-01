using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject menuPauseUI;
    public Button[] buttons;
    public int i;
    float input;

    ColorBlock colorblock;
    Color color;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Escape"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


       

        for (int a = 0; a < buttons.Length; a++)
        {
            if (a != i)
            {
                colorblock = buttons[a].colors;
                color = colorblock.normalColor;
                color.a = 0f;
                colorblock.normalColor = color;
                buttons[a].colors = colorblock;
            }
            else
            {
                 colorblock = buttons[i].colors;
                 color = colorblock.normalColor;
                color.a = 0.7f;
                colorblock.normalColor = color;
                buttons[i].colors = colorblock;


            }

        }


        if (input == 1)
        {
            if (i > 0)
                i--;
            else
                i = buttons.Length - 1;
            input = 0;
        }
        else if (input == -1)
        {
            if (i < buttons.Length - 1)
                i++;
            else
                i = 0;

            input = 0;
        }
        else
        {
          input = Input.GetAxisRaw("Vertical");
        }


        if (Input.GetButtonDown("Fire2"))
        {
            switch (i)
            {
                case 0:
                    Resume();
                    break;
                case 1:
                    buttons[i].gameObject.GetComponent<ControlsAimButton>().ActiveMouseAim();
                    break;

                case 2:
                    buttons[i].gameObject.GetComponent<ControlsAimButton>().ActiveJoystickAim();

                    break;

            }

        }
    }

    public void Resume()
    {
        menuPauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

   public  void Pause()
    {
        menuPauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
