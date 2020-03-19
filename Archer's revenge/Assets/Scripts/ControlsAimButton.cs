using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsAimButton : MonoBehaviour
{
   public void ActiveMouseAim()
    {
        GameManager.Instance.controlindenfy = false;
    }

    public void ActiveJoystickAim()
    {
        GameManager.Instance.controlindenfy = true;
    }
}
