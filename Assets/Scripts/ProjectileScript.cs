﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the cannonball hits the player
            // make them ragdoll
            Player player = collision.gameObject.GetComponent<Player>();
            player.Ragdoll = true;

            // Invoke the players respawn function in 3 seconds
            player.Invoke("Respawn", 3.0f);
        }
    }
}
