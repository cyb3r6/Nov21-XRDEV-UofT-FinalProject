using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Collider collider;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        collider.enabled = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        collider.enabled = false;
    }
}
