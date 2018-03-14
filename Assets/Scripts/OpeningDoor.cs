﻿using System.Collections;
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
        LeftDoorInitialPosition = LeftDoor.transform.position;
        RightDoorInitialPosition = RightDoor.transform.position;

        LeftDoorOpenPosition = LeftDoor.transform.position + new Vector3(0, 0, 0.7f);
        RightDoorOpenPosition = RightDoor.transform.position - new Vector3(0, 0, 0.7f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1.5f))
                if (hit.transform.name == "Button")
                    Opening = !Opening;
        }

        float Step = Speed * Time.deltaTime;
		if (Opening)
        {
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, LeftDoorOpenPosition, Step);
            RightDoor.transform.position = Vector3.MoveTowards(RightDoor.transform.position, RightDoorOpenPosition, Step);
        }
        else
        {
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, LeftDoorInitialPosition, Step);
            RightDoor.transform.position = Vector3.MoveTowards(RightDoor.transform.position, RightDoorInitialPosition, Step);
        }
	}
}