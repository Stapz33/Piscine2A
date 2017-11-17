using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : Bonus_Manager {

    public int Amount;
	// Use this for initialization
    public override void ApplyBonus(Player_Manager player)
    {
        player.Ammo += Amount;
        Destroy(gameObject);
    } 
}
