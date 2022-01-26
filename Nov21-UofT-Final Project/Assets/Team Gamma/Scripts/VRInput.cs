using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInput : MonoBehaviour
{
    [Header("Hand")]
    public Hand hand = Hand.Left;

    public float gripValue;

    public Vector3 velocity;
    public Vector3 angularVelocity;

    public UnityEvent OnGripDown;
    public UnityEvent OnGripUp;

    private string gripAxis;
    private string gripButton;
    private Vector3 previousPosition;
    private Vector3 previousAngularRotation;

    void Start()
    {
        gripAxis = $"XRI_{hand}_Grip";
        gripButton = $"XRI_{hand}_GripButton";
    }

    
    void Update()
    {
        gripValue = Input.GetAxis(gripAxis);

        velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
        angularVelocity = (transform.eulerAngles - previousAngularRotation) / Time.deltaTime;
        previousAngularRotation = transform.eulerAngles;

        if (Input.GetButtonDown(gripButton))
        {
            OnGripDown?.Invoke();
        }
        if (Input.GetButtonUp(gripButton))
        {
            OnGripUp?.Invoke();
        }
    }
}

[System.Serializable]
public enum Hand
{
    Left,
    Right
}