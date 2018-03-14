using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballLiaison : MonoBehaviour {
    public Cloth cloth;

    private ClothSphereColliderPair[] spheres = new ClothSphereColliderPair[20];

    void Start()
    {
        // Initialise ClothSphereColliderPair array
        for (int i = 0; i < 20; i++)
            spheres[i] = new ClothSphereColliderPair();
    }
    // Use this for initialization
    public void RegisterWithCloth(SphereCollider collider)
    {
        // Move all ClothSphereColliderPairs in the array up
        for (int i = 19; i > 0; i--)
            spheres[i] = spheres[i - 1];

        // Add the new ClothSphereColliderPair to the array
        spheres[0] = new ClothSphereColliderPair(collider);

        // Assign the ClothSphereColliderPair array to the cloth
        cloth.sphereColliders = spheres;
    }
}
