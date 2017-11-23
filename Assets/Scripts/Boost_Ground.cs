using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost_Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Player_Manager player = other.gameObject.GetComponentInParent<Player_Manager>();
        if (player)
        {
            player.audioS.outputAudioMixerGroup = player.SFX;
            player.audioS.clip = player.boostS;
            player.audioS.Play();
            player.CanRechargeBoost = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Player_Manager player = other.gameObject.GetComponentInParent<Player_Manager>();
        if (player)
        {
            player.CanRechargeBoost = false;
            player.audioS.Stop();
        }
    }

}
