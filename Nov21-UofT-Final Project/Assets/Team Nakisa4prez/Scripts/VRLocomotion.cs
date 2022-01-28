using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRLocomotion : MonoBehaviour
{
    private VRInput vrInput;
    public Transform XRRig;

    public float playerSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        vrInput = GetComponent<VRInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //HandleRayCast();
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        //Detect if thumbstick pressed

        if(Input.GetButtonDown($"XRI_{vrInput.hand}_Primary2DAxisClick"))
        {
            float rotation = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Horizontal") > 0 ? 30 : -30;

            XRRig.Rotate(0, rotation, 0);
            
        }
    }

    void HandleMovement()
    {
        if (!Input.GetButtonDown($"XRI_{vrInput.hand}_Primary2DAxisClick"))
        {
            
        Vector3 forwardDir = new Vector3(XRRig.forward.x, 0, XRRig.forward.z);
        Vector3 rightDir = new Vector3(XRRig.right.x, 0, XRRig.right.z);

        forwardDir.Normalize();
        rightDir.Normalize();

        float horizontalValue = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Horizontal");
        float verticalValue = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Vertical");

        
            //forward and backwards
            XRRig.position = XRRig.position + (verticalValue * playerSpeed * -forwardDir * Time.deltaTime);

            //strafe right and left
            XRRig.position = XRRig.position + (horizontalValue * playerSpeed * rightDir * Time.deltaTime);
        }
    }


    /* Temporarily comment out raycasting

    void HandleRayCast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //If the ray hits something
        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            lineRenderer.enabled = true;

            CalculateCurve(transform.position, hitInfo.point);
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, hitInfo.point);

            bool validTarget = hitInfo.collider.CompareTag("Teleportation");

            Color color = validTarget ? Color.blue : Color.red;

            //lineRenderer.startColor = color;
            //lineRenderer.endColor = color;
            lineRenderer.material.color = color;


            if (validTarget && Input.GetButtonDown($"XRI_{vrInput.hand}_TriggerButton"))
            {
                StartCoroutine(FadeAndTeleport(hitInfo.point));
            }
        }
        else // if the ray does not hit something
        {
            lineRenderer.enabled = false;
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
            Vector3 curvePoint = Vector3.Lerp(a,b,percent);

            lineRenderer.SetPosition(i, curvePoint);
        }

        lineRenderer.SetPosition(curveSegments - 1, endPoint);
    }

    private IEnumerator FadeAndTeleport(Vector3 teleportPoint)
    {
        float currentTime = 0;

        //Fade to black
        while(currentTime < fadeDuration)
        { 
            blackScreen.color = Color.Lerp(Color.clear, Color.black, currentTime);
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }

        blackScreen.color = Color.black;
        
        //Teleport
        XRRig.position = teleportPoint;
        yield return new WaitForSeconds(1);

        //Fade to Clear
        currentTime = 0;

        while (currentTime < fadeDuration)
        {
            blackScreen.color = Color.Lerp(Color.black, Color.clear, currentTime);
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }
        blackScreen.color = Color.clear;
    }
    */
}
