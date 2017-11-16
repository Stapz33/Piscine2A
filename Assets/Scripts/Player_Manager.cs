﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Manager : MonoBehaviour {

    public static Player_Manager Instance { get; private set; }

	private Rigidbody rb;

    private float BoostUp = 1f;
    private float ForwardAcceleration = 20f;
    private float StraffMaxSpeed = 100f;
    private float smoothXVelocity;
    private float StraffTime = 0.1f;
    private float MaxSpeed = 100f;

    private bool death = false;
    private bool Anim = false;


    private Animator animator;

    public Shoot ProjectilePrefabRed;
    public Shoot ProjectilePrefabPurple;
    public GameObject VFXRed;
    public GameObject VFXPurple;
    public GameObject Player;

    public TimeSpan RunningTime { get { return DateTime.UtcNow - _startedTime; } }

    private DateTime _startedTime;
    // Use this for initialization
    private void Awake()
	{
        Instance = this;
		rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	void Start () {
        _startedTime = DateTime.UtcNow;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		AddMove();

    }

	void AddMove()
	{
        if(!death)
        {

        
        if (Input.GetKeyDown("z") && BoostUp == 1f)
        {
            BoostUp = 1.5f;
        }
        if (Input.GetKeyUp("z") && BoostUp == 1.5f)
        {
            BoostUp = 1f;
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
    private void Update()
    {
        AnimateShip();
        Debug.Log(RunningTime);
        ShootRed();
        ShootPurple();
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
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        death = true;
        Camera_Manager.Instance.DeathScreen();
        Destroy(Player);
    }

    public void Win()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        death = true;
        Camera_Manager.Instance.WinScreen();
    }

    void ShootRed()
    {
        if(!death)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SpawnProjectileRed();
            }
        }
        
    }
    void ShootPurple()
    {
        if (!death)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                SpawnProjectilePurple();
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
    }
    public void SpawnProjectilePurple()
    {
        Shoot projectile = (Shoot)Instantiate(ProjectilePrefabPurple, transform.position, Quaternion.Euler(0, 0, 0));
        Vector3 initialVelocity = rb.velocity;
        initialVelocity.x = 0f;
        initialVelocity.z = 0f;
        projectile.Fire(rb.velocity);
    }
}
