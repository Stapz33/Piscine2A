using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Manager : MonoBehaviour {

    private float MaxSpeed = 100f;

	private Rigidbody rb;
    private float ForwardAcceleration = 20f;
    private float StraffMaxSpeed = 100f;
    private float smoothXVelocity;
    private float StraffTime = 0.1f;

    // Use this for initialization
    private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		AddMove();
	}

	void AddMove()
	{
        Vector3 newVelocity = rb.velocity;
        if(newVelocity.z > MaxSpeed)
        {
            newVelocity.z = MaxSpeed;
        }
        else
        {
            newVelocity.z += ForwardAcceleration * Time.fixedDeltaTime;
        }
        float targetVelocity = Input.GetAxis("Horizontal") * StraffMaxSpeed;

        newVelocity.x = Mathf.SmoothDamp(newVelocity.x, targetVelocity, ref smoothXVelocity, StraffTime);

        rb.velocity = newVelocity;

        
	}

    private void LateUpdate()
    {
        Debug.Log(rb.velocity.z);
    }
}
