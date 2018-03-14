using System.Collections;
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
            // Ragdol the player if it enters the trigger
            Player player = other.GetComponent<Player>();
            player.Ragdoll = true;

            // Invoke the players respawn funciton in 3 seconds
            player.Invoke("Respawn", 3.0f);
        }
        // Destroy any projectiles that enter the trigger
        else if (other.CompareTag("Projectile"))
            Destroy(other.gameObject);
    }
}
