using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
    private Vector3 InitialPosition;

    [Range(0, float.MaxValue), Space(10)]
    public float MovingDistanceX = 0;
    public bool NegativeX = false;

    [Range(0, float.MaxValue), Space(10)]
    public float MovingDistanceY = 0;
    public bool NegativeY = false;

    [Range(0, float.MaxValue), Space(10)]
    public float MovingDistanceZ = 0;
    public bool NegativeZ = false;

    // Use this for initialization
    void Start () {
        InitialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = InitialPosition
            + (MovingDistanceX == 0 ? 0 : Mathf.PingPong(Time.time, MovingDistanceX)) * Vector3.right * (NegativeX ? -1 : 1)
            + (MovingDistanceY == 0 ? 0 : Mathf.PingPong(Time.time, MovingDistanceY)) * Vector3.up * (NegativeY ? -1 : 1)
            + (MovingDistanceZ == 0 ? 0 : Mathf.PingPong(Time.time, MovingDistanceZ)) * Vector3.forward * (NegativeZ ? -1 : 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = gameObject.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = null;
    }
}
