using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 3.0f))
                if (hit.transform == transform)
                {
                    CannonScript cs = hit.transform.parent.GetComponent<CannonScript>();
                    cs.Shoot();
                }
        }
    }
}
