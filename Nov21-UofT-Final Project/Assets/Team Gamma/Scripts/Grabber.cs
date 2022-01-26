using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
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

    public float throwForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab)
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
            collidingObject.OnHoverEnd();
            collidingObject = null;
        }
    }

    public void Grab()
    {
        if(collidingObject != null)
        {
            heldObject = collidingObject;
            heldObject.ParentGrab(controller);
        }
    }

    public void Release()
    {
        if(heldObject)
        {
            heldObject.ParentRelease();

            // throw
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

    
    void Update()
    {
        
    }

}