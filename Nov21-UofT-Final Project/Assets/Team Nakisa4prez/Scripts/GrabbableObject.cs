using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableObject : MonoBehaviour
{
    public Rigidbody grabbableRigidbody;
    public Renderer grabbableRenderer;
    public Color hoverColor;
    public Color nonHoverColor;
    public GameObject sphereObject;
    protected bool objectGrabbed;

    public void OnHoverStart()
    {
        if (tag != "GrabbableCube")
        {
            grabbableRenderer.material.color = hoverColor;
        }
    }

    public void OnHoverEnd()
    {
        if (tag != "GrabbableCube")
        {
            grabbableRenderer.material.color = nonHoverColor;
        }
    }

    public void ParentGrab(VRInput controller)
    {
        objectGrabbed = true;
        transform.SetParent(controller.transform);
        //grabbableRigidbody.useGravity = false;
        grabbableRigidbody.isKinematic = true;
        if(tag == "GrabbableCube")
        {
            sphereObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void ParentRelease()
    {
        objectGrabbed = false;
        transform.SetParent(null);
        //grabbableRigidbody.useGravity = true;
        grabbableRigidbody.isKinematic = false;

    }

}
