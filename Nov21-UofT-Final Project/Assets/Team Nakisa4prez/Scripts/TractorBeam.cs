using UnityEngine;

public class TractorBeam : GrabbableObject
{
    private LineRenderer lineRenderer;

    public float curveHeight = 2;
    public int curveSegments = 20;
    private GameObject currentTarget;
    public Vector3 hitInfotest;
    private bool tractorLock;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //start disabled
        lineRenderer.enabled = false;
        lineRenderer.positionCount = curveSegments;
    }

    private void Update()
    {
        if (objectGrabbed == true)
        {
            if (Input.GetButton("XRI_Right_TriggerButton") || Input.GetButton("XRI_Left_TriggerButton"))
            {
                Debug.Log("Trigger Pressed");
                HandleTractorBeam();
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void HandleTractorBeam()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        lineRenderer.enabled = true;

        //If the ray hits something
        if (Physics.Raycast(ray, out hitInfo, 500))
        {
            CalculateCurve(transform.position, hitInfo.point);
            bool validTarget = hitInfo.collider.CompareTag("TractorableObject");

            if (validTarget || tractorLock == true)
            {
                //Beam Hitting Moveable Target
                lineRenderer.material.color = Color.blue;
                

                if (validTarget) { 
                currentTarget = hitInfo.transform.gameObject;
                }

                if (Input.GetButton("XRI_Left_PrimaryButton") || Input.GetButton("XRI_Right_PrimaryButton"))
                {
                    //Lock Target Object to Tractor Beam
                    var fixedJoint = currentTarget.GetComponent<FixedJoint>();
                    fixedJoint.connectedBody = hitInfo.collider.attachedRigidbody;
                    currentTarget.transform.position = hitInfo.point;
                    tractorLock = true;
                    currentTarget.GetComponent<StackableCube>().OnHoverStart();
                    lineRenderer.material.color = Color.green;

                    //fix for box swap bug
                    if (validTarget != currentTarget)
                    {
                        currentTarget.GetComponent<StackableCube>().OnHoverEnd();
                    }
                }
                else
                {
                    //deselect object if PrimaryButton not down
                    currentTarget.GetComponent<FixedJoint>().connectedBody = null;
                    currentTarget.GetComponent<StackableCube>().OnHoverEnd();
                }
            }
            else
            {
                //reset if not valid Target
                currentTarget.GetComponent<FixedJoint>().connectedBody = null;
                lineRenderer.material.color = Color.red;
                currentTarget.GetComponent<StackableCube>().OnHoverEnd();
            }
        }
    }

    void CalculateCurve(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 midPoint = (startPoint + endPoint) / 2;

        Vector3 controlPoint = midPoint + Vector3.up * curveHeight;

        for (int i = 0; i < curveSegments; i++)
        {
            float percent = i / (float)curveSegments;

            Vector3 a = Vector3.Lerp(startPoint, controlPoint, percent);
            Vector3 b = Vector3.Lerp(controlPoint, endPoint, percent);
            Vector3 curvePoint = Vector3.Lerp(a, b, percent);

            lineRenderer.SetPosition(i, curvePoint);
        }

        lineRenderer.SetPosition(curveSegments - 1, endPoint);
    }
}
