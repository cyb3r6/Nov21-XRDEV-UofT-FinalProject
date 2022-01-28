using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour
{
    protected Rigidbody grabbableRigidbody;
    protected Renderer grabbableRenderer;
    public bool tractorLocked;
    public Material hoverColor;
    public Color nonHoverColor;

    public void OnHoverStart()
    {
        if (tag == "TractorableObject")
        {
            grabbableRenderer.material = hoverColor;
        }
    }

    public void OnHoverEnd()
    {
        if (tag == "TractorableObject")
        {
            grabbableRenderer.material = null;
            grabbableRenderer.material.color = nonHoverColor;
        }
    }

    public void ParentTractorLock(VRInput controller)
    {
        tractorLocked = true;
        transform.SetParent(controller.transform);
        //grabbableRigidbody.useGravity = false;
        grabbableRigidbody.isKinematic = true;
    }

    public void ParentTractorRelease()
    {
        tractorLocked = false;
        transform.SetParent(null);
        //grabbableRigidbody.useGravity = true;
        grabbableRigidbody.isKinematic = false;
    }

}
