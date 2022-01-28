using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteractable : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPressedEvent : UnityEvent { };
    [System.Serializable]
    public class ButtonReleased : UnityEvent { };

    public ButtonPressedEvent onButtonPressed;
    public ButtonReleased onButtonReleased;

    public Vector3 Axis = new Vector3(0, -1, 0);
    public float maxDistance;
    public float returnSpeed = 1f;

    public AudioClip buttonPressedClip;
    public AudioClip buttonReleasedClip;

    private Vector3 startPosition;
    private Rigidbody rigidbody;
    private Collider collider;
    private bool pressed = false;

    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
        startPosition = transform.position;

    }

    
    void FixedUpdate()
    {
        Vector3 worldAxis = transform.TransformDirection(Axis);
        Vector3 end = transform.position + worldAxis * maxDistance;

        float currentDistance = (transform.position - startPosition).magnitude;
        RaycastHit hitInfo;

        float move = 0.0f;

        if(rigidbody.SweepTest(-worldAxis, out hitInfo, returnSpeed * Time.deltaTime))
        {
            // hitting something if the contact is < mean we pressed, move downward
            move = (returnSpeed * Time.deltaTime) - hitInfo.distance;
        }
        else
        {
            move -= returnSpeed * Time.deltaTime;
        }

        float newDistance = Mathf.Clamp(currentDistance + move, 0, maxDistance);
        rigidbody.position = startPosition + worldAxis * newDistance;

        if(!pressed && Mathf.Approximately(newDistance, maxDistance))
        {
            pressed = true;
            onButtonPressed?.Invoke();

        }
        else if(pressed && !Mathf.Approximately(newDistance, maxDistance))
        {
            pressed = false;
            onButtonReleased?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        //handles
    }
}
