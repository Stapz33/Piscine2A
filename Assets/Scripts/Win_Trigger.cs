﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Trigger : MonoBehaviour {

    // Use this for initialization
    public void OnCollisionEnter(Collision collision)
    {
        Player_Manager player = collision.gameObject.GetComponentInParent<Player_Manager>();
        if (player)
        {
            player.Win();
        }
    }
}
