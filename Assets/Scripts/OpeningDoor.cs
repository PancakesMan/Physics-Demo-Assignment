using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour {

    public GameObject LeftDoor, RightDoor;
    public float Speed = 1;
    public bool Opening = false;

    private Vector3 LeftDoorInitialPosition, RightDoorInitialPosition;
    private Vector3 LeftDoorOpenPosition, RightDoorOpenPosition;
	// Use this for initialization
	void Start () {
        // Store initial positions of doors
        LeftDoorInitialPosition = LeftDoor.transform.position;
        RightDoorInitialPosition = RightDoor.transform.position;

        // Store positions of doors when they're open
        LeftDoorOpenPosition = LeftDoor.transform.position + new Vector3(0, 0, 0.7f);
        RightDoorOpenPosition = RightDoor.transform.position - new Vector3(0, 0, 0.7f);
    }
	
	// Update is called once per frame
	void Update () {

        // If mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // And the ray hits the doors button
            // open the door
            if (Physics.Raycast(ray, out hit, 2.0f))
                if (hit.transform.name == "Button" && hit.transform.IsChildOf(transform))
                    Opening = !Opening;
        }

        float Step = Speed * Time.deltaTime;
		if (Opening)
        {
            // Move the doors to their open positions by step
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, LeftDoorOpenPosition, Step);
            RightDoor.transform.position = Vector3.MoveTowards(RightDoor.transform.position, RightDoorOpenPosition, Step);
        }
        else
        {
            // Move the doors to their initial positions by step
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, LeftDoorInitialPosition, Step);
            RightDoor.transform.position = Vector3.MoveTowards(RightDoor.transform.position, RightDoorInitialPosition, Step);
        }
	}
}
