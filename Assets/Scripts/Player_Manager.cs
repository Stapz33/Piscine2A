using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Player_Manager : MonoBehaviour {

    public static Player_Manager Instance { get; private set; }

	private Rigidbody rb;

    private float BoostUp = 1f;
    private float StraffMaxSpeed = 100f;
    private float smoothXVelocity;
    private float StraffTime = 0.1f;
    public float MaxSpeed = 100f;
    public AudioSource audioS;
    public AudioClip ShieldS, deathS, boostS, shieldHit;
    public AudioMixerGroup SFX;
    public AudioMixerGroup endGame;
    public AudioMixerGroup inGame;
    public int Ammo = 3;
    public bool Shield;
    public AudioSource SoundManager;
    public AudioClip[] Musics;
    public TextMeshProUGUI TimerEnd;
    public AudioSource MusicMix;
    public float TimerInit;
    private float timerActive;

    private bool canShoot = true;

    private bool death = false;
    private bool Anim = false;


    private Animator animator;

    public Shoot ProjectilePrefabRed;
    public Shoot ProjectilePrefabPurple;
    public GameObject Player;
    public GameObject VFXShield;
    public TextMeshProUGUI timerTime;
    public GameObject imageBoost;
    public GameObject shieldImage;
    public float BoostActive;
    public float BoostMax = 100f;
    public bool CanRechargeBoost = false;
    
    public TimeSpan RunningTime { get { return DateTime.UtcNow - _startedTime; } }

    private DateTime _startedTime;
    private bool StopBoost;

    // Use this for initialization
    private void Awake()
	{
        timerActive = TimerInit;
        SoundManager.outputAudioMixerGroup = inGame;
        SoundManager.clip = Musics[UnityEngine.Random.Range(0, 3)];
        SoundManager.Play();
        Instance = this;
		rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
	}
	void Start () {
        _startedTime = DateTime.UtcNow;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		AddMove();

    }

    public void BoostSpeed()
    {
        if(BoostActive < BoostMax)
        {
            
            BoostActive += 0.6f;
        }
    }

    public float NumberBoost()
    {
        return BoostActive;
    }

	void AddMove()
	{
        if(!death)
        {

        
        if (Input.GetKeyDown("space") && BoostUp == 1f && BoostActive > 0f)
        {
                imageBoost.SetActive(true);
                StopBoost = true;
                
        }
        if (Input.GetKeyUp("space") && BoostUp == 2f || BoostActive == 0f)
        {
                imageBoost.SetActive(false);
                BoostUp = 1f;
                StopBoost = false;
            }
        if (Input.GetKeyDown("s") && BoostUp == 1f)
        {
            BoostUp = 0.5f;
        }
        if (Input.GetKeyUp("s") && BoostUp == 0.5f)
        {
            BoostUp = 1f;
        }
        Vector3 newVelocity = rb.velocity;
         newVelocity.z = MaxSpeed * BoostUp;


        float targetVelocity = Input.GetAxis("Horizontal") * StraffMaxSpeed;

        newVelocity.x = Mathf.SmoothDamp(newVelocity.x, targetVelocity, ref smoothXVelocity, StraffTime) ;
        

        rb.velocity = newVelocity;
        }

    }

    public void ShieldActive()
    {
        if (Shield)
        {
            shieldImage.SetActive(true);
            VFXShield.SetActive(true);
        }
        if (!Shield)
        {
            shieldImage.SetActive(false);
            VFXShield.SetActive(false);
        }
    }
    private void Update()
    {
        AnimateShip();
        ShootRed();
        ShootPurple();
        if (!death)
        {
            Timer();
        }
        TimerShoot();



        if (BoostActive >= BoostMax)
        {
            BoostActive = BoostMax;
        }
        if (BoostActive <= 0)
        {
            BoostActive = 0;
        }

        if (CanRechargeBoost == true)
        {
            BoostSpeed();
        }

        if(StopBoost == true && BoostActive > 0f)
        {

            BoostUp = 2f;
            BoostActive -= 0.2f;
            
        }
    }

    private void Timer()
    {
        timerTime.text = RunningTime.Minutes.ToString("00") + ":" + RunningTime.Seconds.ToString("00") + ":" + RunningTime.Milliseconds.ToString("00").Substring(0,2);
        TimerEnd.text = "Time : " + timerTime.text;
    }

    void AnimateShip()
    {
        if(!death)
        {
        if (Input.GetKeyDown("d"))
        {
          
            animator.SetTrigger("MoveRight");
        }
        if (Input.GetKeyUp("d"))
        {
            animator.SetTrigger("ResetRight");
        }
        if (Input.GetKeyDown("q"))
        {
            animator.SetTrigger("MoveLeft");
        }
        if (Input.GetKeyUp("q"))
        {
            animator.SetTrigger("ResetLeft");
        }
        }
        if(death && !Anim)
        {
            animator.SetTrigger("ResetRight");
            animator.SetTrigger("ResetLeft");
            Anim = true;

        }

    }

    private void LateUpdate()
    {
        Debug.Log(rb.velocity.z);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Structure"))
        {
            SceneManager.LoadScene("Killian");
        }
    }*/
    public void Kill()
    {

        SoundManager.outputAudioMixerGroup = endGame;
        audioS.clip = deathS;
        audioS.Play();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        death = true;
        Camera_Manager.Instance.DeathScreen();
        Destroy(Player);
        UI_Manager.Instance.DispLose();

    }

    public void Win()
    {
        BoostUp = 1f;
        SoundManager.outputAudioMixerGroup = endGame;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        death = true;
        UI_Manager.Instance.DispWin();
        Camera_Manager.Instance.WinScreen();
    }

    void ShootRed()
    {
        if(!death)
        {
            if (Input.GetButtonDown("Fire1") && Ammo > 0)
            {
                if (canShoot == true)
                {
                    SpawnProjectileRed();
                    canShoot = false;
                }

            }
        }
        
    }
    void ShootPurple()
    {
        if (!death)
        {
            if (Input.GetButtonDown("Fire2") && Ammo > 0)
            {
                if (canShoot == true)
                {
                    SpawnProjectilePurple();
                    canShoot = false;
                }
               
            }
        }

    }

    public void SpawnProjectileRed()
    {
        Shoot projectile = (Shoot)Instantiate(ProjectilePrefabRed, transform.position, Quaternion.Euler(0,0,0));
        Vector3 initialVelocity = rb.velocity;
        initialVelocity.x = 0f;
        initialVelocity.z = 0f;
        projectile.Fire(rb.velocity);
        Ammo--;
    }
    public void SpawnProjectilePurple()
    {
        Shoot projectile = (Shoot)Instantiate(ProjectilePrefabPurple, transform.position, Quaternion.Euler(0, 0, 0));
        Vector3 initialVelocity = rb.velocity;
        initialVelocity.x = 0f;
        initialVelocity.z = 0f;
        projectile.Fire(rb.velocity);
        Ammo--;
    }
    public void SoundSFX()
    {
        audioS.outputAudioMixerGroup = SFX;
        audioS.clip = ShieldS;
        audioS.Play();
    }

    void TimerShoot()
    {
        if (canShoot == false)
        {
            timerActive -= Time.deltaTime;
            if (timerActive <= 0)
            {
                canShoot = true;
                timerActive = TimerInit;
            }
        }
    }
    

}
