using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : Bonus_Manager {

    public int Amount = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void ApplyBonus(Player_Manager player)
    {
        player.Ammo += Amount;
        Destroy(gameObject);
    } 
}
