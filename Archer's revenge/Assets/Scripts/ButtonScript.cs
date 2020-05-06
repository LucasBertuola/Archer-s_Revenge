using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

    public class ButtonScript : MonoBehaviour {

    public GameObject fadeObj;

    public void PlayClick()
    {
        fadeObj.SetActive(true);
        Invoke("ChangeLevel", 2);
    }

    public void ExitClick()
    {
        fadeObj.SetActive(true);
        Invoke("Quit", 2);
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Quit()
    {
        Application.Quit();
    }


}
