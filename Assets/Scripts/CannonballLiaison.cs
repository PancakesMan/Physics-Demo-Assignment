using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballLiaison : MonoBehaviour {
    public Cloth cloth;

    private ClothSphereColliderPair[] spheres = new ClothSphereColliderPair[20];

    void Start()
    {
        for (int i = 0; i < 20; i++)
            spheres[i] = new ClothSphereColliderPair();
    }
    // Use this for initialization
    public void RegisterWithCloth(SphereCollider collider)
    {
        for (int i = 19; i > 0; i--)
            spheres[i] = spheres[i - 1];
        spheres[0] = new ClothSphereColliderPair(collider);

        cloth.sphereColliders = spheres;
    }
}
