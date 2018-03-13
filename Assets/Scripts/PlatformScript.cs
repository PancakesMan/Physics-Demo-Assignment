using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
    private ConfigurableJoint joint;

	// Use this for initialization
	void Start () {
        joint = GetComponent<ConfigurableJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        Vector3 old = joint.targetPosition;
        if (collision.rigidbody.tag == "StillPlatform")
            joint.targetPosition = new Vector3(-old.x, old.y, old.z);
    }
}
