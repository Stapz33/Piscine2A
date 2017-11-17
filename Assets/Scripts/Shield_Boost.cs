using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Boost : Bonus_Manager {

    // Use this for initialization
    public override void ApplyBonus(Player_Manager player)
    {
        player.Shield = true;
        player.ShieldActive();
        Destroy(gameObject);
    }
}
