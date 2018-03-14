﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Ragdoll = true;

            player.Invoke("Respawn", 3.0f);
        }
        else if (other.CompareTag("Projectile"))
            Destroy(other.gameObject);
    }
}
