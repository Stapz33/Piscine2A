using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Trigger : MonoBehaviour, ITakeDamage
{

    public Shoot.ColorType Type;

    // Use this for initialization
    public void OnCollisionEnter(Collision collision)
    {
        Player_Manager player = collision.gameObject.GetComponentInParent<Player_Manager>();
        if (player)
        {
            player.Kill();
        }
    }

    public float MaxHelath = 100f;

    private float _currentHealth = 0f;

    // Use this for initialization
    void Start()
    {
        _currentHealth = MaxHelath;
    }

    public void TakeDamage(float damage, GameObject instigator)
    {
        Shoot projectile = instigator.GetComponent<Shoot>();
        if(projectile)
        {
            if(Type == projectile.Type)
            {
                _currentHealth -= damage;
                if (_currentHealth <= 0f)
                {
                    Kill();
                }
            }
        }
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}

