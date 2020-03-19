using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour {
    #region singleton
    public static GameManager Instance;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    #endregion

    public int enemyCount;
    public int enemyKillCount;
    public int key;
    public bool minotaur2ndFase;

    public GameObject cave;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject tutorialTrigger;
    public GameObject caveTrigger;
    public GameObject gateTrigger;
    public SpriteRenderer caveRend;
    public Color caveColor;
    public AudioClip bossMusic;
    public AudioClip winMusic;
    public AudioClip gameOverMusic;
    public AudioClip leverSound;
    public AudioSource source;
    public Animator bossAnim;
    public Animator gateAnim;
    public GameObject healthBar;
    public GameObject leverOpen;
    public GameObject leverClosed;
    public GameObject spikes;
    public GameObject killCountPanel;
    public GameObject invisibleWall;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public Image keyImage;
    public Text killCount;
    public Text keyCount;
    public bool controlindenfy = false;
    public GameObject aimMouse;
    public GameObject aimJoystick;
    public void Start()
    {
        minotaur2ndFase = false;
        caveRend = cave.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        killCount.text = enemyKillCount.ToString();
        keyCount.text = key.ToString();


        if (enemyKillCount == 0)
        {
            enemyCount = 200;
            killCountPanel.SetActive(false);
        }
        if (key == 1)
        {
            keyImage.color = Color.white;
        }
        else
        {
            keyImage.color = Color.gray;
        }

    }

    public void DisableAimMouse()
    {
        aimJoystick.SetActive(true);
        aimMouse.SetActive(false);
        
    }

    public void DisableAimJoystick()
    {
        aimMouse.SetActive(true);
        aimJoystick.SetActive(false);
       
    }

 
    public void EnterCave()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        cam3.SetActive(false);
        bossAnim.SetTrigger("walk");
        caveRend.color = caveColor;
        source.clip = bossMusic;
        source.Play();
        healthBar.SetActive(true);
        invisibleWall.SetActive(true);
        Destroy(caveTrigger.gameObject);
    }

    public void EnterForest()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
        enemyCount = 0;
        spikes.SetActive(true);
        Destroy(tutorialTrigger.gameObject);
    }


    public void RetractSpikes()
    {
        leverOpen.SetActive(true);
        AudioSource.PlayClipAtPoint(leverSound , leverOpen.transform.position);
        spikes.SetActive(false);
        Destroy(leverClosed.gameObject);
    }

    public void OpenGate()
    {
        gateAnim.SetTrigger("open");
        Destroy(gateTrigger.gameObject);
    }

    public void IsDead()
    {
        gameOverScreen.SetActive(true);
        source.clip = gameOverMusic;
        source.loop = false;
        source.Play();
        Invoke("ReturnMenu", 5);
    }

    public void Won()
    {
        winScreen.SetActive(true);
        source.clip = winMusic;
        source.loop = false;
        source.Play();
        Invoke("ReturnMenu", 5);
    }

    public void ReturnMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }
}
