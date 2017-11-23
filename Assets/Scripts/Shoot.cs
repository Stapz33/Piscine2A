using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public enum ColorType
    {
        Red,
        Purple,
        None
    }

    public float Damage = 100f;
    public ColorType Type;
    public GameObject VFX;


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
        Instantiate(VFX, transform.position, Quaternion.Euler(0, 0, 0));
        Player_Manager.Instance.DestroyProjectileFX();
        Destroy(this.gameObject);

    }
}
