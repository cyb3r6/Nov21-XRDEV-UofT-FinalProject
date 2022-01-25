using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody grabbableRigidbody;
    public Renderer grabbableRenderer;

    public void ParentGrab(VRInput controller)
    {
        transform.SetParent(controller.transform);
        grabbableRigidbody.useGravity = false;
        grabbableRigidbody.isKinematic = true;
    }

    public void ParentRelease()
    {
        transform.SetParent(null);
        grabbableRigidbody.useGravity = true;
        grabbableRigidbody.isKinematic = false;
    }
}
