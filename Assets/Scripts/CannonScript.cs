using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    public GameObject Cannonball;
    public float Force = 1000;
    public float FireRate = 3.0f;

    float timer = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= FireRate)
        {
            timer = 0.0f;
            Shoot();
        }
	}

    public void Shoot()
    {
        // Copy the Cannonball projectile
        GameObject projectile = Instantiate(Cannonball, Cannonball.transform.position, Cannonball.transform.rotation);

        // Get the Cannonball's RigidBody
        Rigidbody body = projectile.GetComponent<Rigidbody>();

        // Enable the Cannonballs collider and renderer
        projectile.GetComponent<SphereCollider>().enabled = true;
        projectile.GetComponent<MeshRenderer>().enabled = true;

        // If the cannon has a CannonballLiaison script attached
        CannonballLiaison liaison = GetComponent<CannonballLiaison>();
        if (liaison != null)
            // Register the Cannonball with the CannonballLiaison
            liaison.RegisterWithCloth(projectile.GetComponent<SphereCollider>());
        
        // Make the Cannonball non-kinematic
        // and apply force to it
        body.isKinematic = false;
        body.AddForce(Cannonball.transform.up * Force);
    }
}
