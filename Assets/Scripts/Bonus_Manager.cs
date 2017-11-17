using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] 
public abstract class Bonus_Manager : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Player_Manager player = other.gameObject.GetComponentInParent<Player_Manager>();
        if (player != null)
        {
            ApplyBonus (player);
        }
    }

    public virtual void ApplyBonus(Player_Manager player)
    {

    }
}
