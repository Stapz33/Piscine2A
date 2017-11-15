using System;
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
    private int activeCamera = 0;

    private Animator animator;


    public GameObject cameraFPS;
    public GameObject cameraTPS;
    public GameObject cameraTOP;

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
        if(newVelocity.z > MaxSpeed)
        {
            newVelocity.z = MaxSpeed * BoostUp;
        }
        else
        {
            newVelocity.z += ForwardAcceleration * Time.fixedDeltaTime * BoostUp;
        }
        float targetVelocity = Input.GetAxis("Horizontal") * StraffMaxSpeed;

        newVelocity.x = Mathf.SmoothDamp(newVelocity.x, targetVelocity, ref smoothXVelocity, StraffTime) ;
        

        rb.velocity = newVelocity;

        
	}
    private void Update()
    {
        ChangeCamera();
        AnimateShip();
        Debug.Log(RunningTime);
    }

    void AnimateShip()
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

    private void LateUpdate()
    {
        Debug.Log(rb.velocity.z);
    }

    void ChangeCamera()
    {
        if (Input.GetKeyDown("c"))
        {
            if (activeCamera == 0)
            {
                cameraFPS.SetActive(false);
                cameraTPS.SetActive(true);
                activeCamera += 1;
                return ;
            }
            if (activeCamera == 1)
            {
                cameraTPS.SetActive(false);
                cameraTOP.SetActive(true);
                activeCamera += 1;
                return ;
            }
            if (activeCamera == 2)
            {
                cameraTOP.SetActive(false);
                cameraFPS.SetActive(true);
                activeCamera = 0;
                return ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Structure"))
        {
            SceneManager.LoadScene("Killian");
        }
    }
}
