using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossbowScript : MonoBehaviour {

    private GameManager gameManager;

    public Slider ChargeBar;
    public Image ChargeBarFill;

    public float offset;
    public GameObject bolt;
    public GameObject specialBolt;
    public AudioClip shootingSoung;
    public AudioClip shootingSpecialSoung;
    private AudioSource source;
    public Transform boltSpawn;
    public float boltCD = 0;
    public float fireDelay = 0.5f;

    public bool joystick;

    public Transform cursor;
    public Color colorC;
    private Color colorN;

    private float lerpTimer;

    void Start()
    {
        source = GetComponent<AudioSource>();
        colorN = ChargeBarFill.color;
    }

    void Update () {

        joystick = GameManager.Instance.controlindenfy;

        Vector2 crossbowScale = transform.localScale;
        Vector2 cursorPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float worldXPos = Camera.main.ScreenToWorldPoint(cursorPos).x;

        if (worldXPos > gameObject.transform.position.x)
        {
            crossbowScale.x = 1;
            crossbowScale.y = 1;
            transform.localScale = crossbowScale;
        }
        else
        {
            crossbowScale.x = -1;
            crossbowScale.y = -1;
            transform.localScale = crossbowScale;
        }


        boltCD -= Time.deltaTime;

        Vector3 target = new Vector2(0,0) ;

        //Keyboard
        if (!joystick)
        {
             target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }


        //Joystick
        if (joystick)
        {
             target = cursor.position -transform.position;
        }


        Vector3 difference = target;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);


        if (Input.GetAxisRaw("Fire1") == 1 && boltCD <= 0f)
        {
            Instantiate(bolt, boltSpawn.position, transform.rotation);
            source.PlayOneShot(shootingSoung);
            boltCD = fireDelay;

        }

        if (Input.GetAxisRaw("Fire3") == 1)
        {
            timecharge += Time.deltaTime;
            ChargeBar.value = timecharge;

            if(timecharge >= charge)
            {
                lerpTimer = 1;
                ChargeBarFill.color = colorC;
            }

        }


        if (Input.GetAxisRaw("Fire3") == 0 && timecharge >= charge)
        {
            timecharge = 0;
            lerpTimer = 0;
            ShootSpecialBolt();
            ChargeBar.value = timecharge;
             ChargeBarFill.color = colorN;


        }
        else if (Input.GetAxisRaw("Fire3") == 0 && timecharge < charge)
        {
            lerpTimer = 0;

            timecharge = 0;
            ChargeBar.value = timecharge;
            ChargeBarFill.color = colorN;

        }

        if (lerpTimer > 0)
        {
            ChargeBarFill.color = Color.Lerp(colorC, colorN, Mathf.PingPong(Time.time, 0.5f));
            lerpTimer -= Time.deltaTime;
        }

    }

    float timecharge =0;
    float charge = 2f;
    void ShootSpecialBolt()
    {
        Instantiate(specialBolt, boltSpawn.position, transform.rotation);
        source.PlayOneShot(shootingSpecialSoung);
        boltCD = fireDelay;
    }
}
