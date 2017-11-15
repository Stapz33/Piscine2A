using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float Damage = 10f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire(Vector3 initialVelocity)
    {
        _rigidbody.velocity = initialVelocity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        ITakeDamage damageable = collision.gameObject.GetComponentInParent<ITakeDamage>();
        if (damageable != null)
        {
            damageable.TakeDamage(Damage, this.gameObject);
        }

        Destroy(this.gameObject);
    }
}
