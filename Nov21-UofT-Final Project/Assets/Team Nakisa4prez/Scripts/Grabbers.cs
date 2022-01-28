using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbers : MonoBehaviour
{
    /// <summary>
    /// What we're touching
    /// </summary>
    public GrabbableObject collidingObject;

    /// <summary>
    /// What we're holding
    /// </summary>
    public GrabbableObject heldObject;

    public VRInput controller;

    public float throwForce;

    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if(grab)
        {
            collidingObject = grab;
            collidingObject.OnHoverStart();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab==collidingObject)
        {
            collidingObject?.OnHoverEnd();
            collidingObject = null;
        }
    }

    public void Grab()
    {
        if(collidingObject != null)
        {
            heldObject = collidingObject;
            heldObject.ParentGrab(controller);
            Debug.Log("Grab!");
        }
    }

    public void Release()
    {
        if (heldObject)
        {
            Debug.Log("Release");
            heldObject.ParentRelease();
            //throw
            //heldObject.grabbableRigidbody.velocity = controller.velocity * throwForce;
            //heldObject.grabbableRigidbody.angularVelocity = controller.angularVelocity * throwForce;
            heldObject = null;
        }
    }

    private void Start()
    {
        controller = GetComponent<VRInput>();
        controller.OnGripDown.AddListener(Grab);
        controller.OnGripUp.AddListener(Release);
    }

    void Update()
    {
        
    }
}
