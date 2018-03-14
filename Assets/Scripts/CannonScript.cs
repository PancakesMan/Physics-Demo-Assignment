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
        GameObject projectile = Instantiate(Cannonball, Cannonball.transform.position, Cannonball.transform.rotation);
        Rigidbody body = projectile.GetComponent<Rigidbody>();
        projectile.GetComponent<SphereCollider>().enabled = true;
        projectile.GetComponent<MeshRenderer>().enabled = true;

        CannonballLiaison liaison = GetComponent<CannonballLiaison>();
        if (liaison != null)
            liaison.RegisterWithCloth(projectile.GetComponent<SphereCollider>());
        
        body.isKinematic = false;
        body.AddForce(Cannonball.transform.up * Force);
    }
}
