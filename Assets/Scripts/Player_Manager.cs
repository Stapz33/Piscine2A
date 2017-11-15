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
    

    private Animator animator;

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
    private void Update()
    {
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

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Structure"))
        {
            SceneManager.LoadScene("Killian");
        }
    }*/
    public void Kill()
    {
        Camera_Manager.Instance.DeathScreen();
        Destroy(Player);
    }

    public void Win()
    {
        Camera_Manager.Instance.DeathScreen();
    }
}
