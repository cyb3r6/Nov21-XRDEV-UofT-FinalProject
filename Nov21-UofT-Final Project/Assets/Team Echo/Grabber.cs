using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public BallScript collidingObject;
    public BallScript heldObject;

    public VRInput controller;

    public float throwForce = 5f;
    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<BallScript>();
        if (grab)
        {
            collidingObject = grab;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var grab = other.GetComponent<BallScript>();
        if (grab == collidingObject)
        {
            collidingObject = null;
        }
    }

    public void Grab()
    {
        if (collidingObject != null)
        {
            heldObject = collidingObject;
            heldObject.ParentGrab(controller);
        }
    }

    public void Release()
    {
        if (heldObject)
        {
            heldObject.ParentRelease();

            heldObject.grabbableRigidbody.velocity = controller.velocity * throwForce;
            heldObject.grabbableRigidbody.angularVelocity = controller.angularVelocity * throwForce;
            heldObject = null;
        }
    }
    void Start()
    {
        controller = GetComponent<VRInput>();

        controller.OnGripDown.AddListener(Grab);
        controller.OnGripUp.AddListener(Release);
    }
}
