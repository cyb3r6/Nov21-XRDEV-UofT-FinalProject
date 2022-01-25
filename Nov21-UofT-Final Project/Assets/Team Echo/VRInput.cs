using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInput : MonoBehaviour
{
    public Hand hand = Hand.Left;

    public float gripValue;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public UnityEvent OnGripDown;
    public UnityEvent OnGripUp;

    private string gripAxis;
    private string gripButton;
    private Vector3 prevPos;
    private Vector3 prevAngRot;

    void Start()
    {
        gripAxis = $"XRI_{hand}_Grip";
        gripButton = $"XRI_{hand}_GripButton";
    }


    void Update()
    {
        gripValue = Input.GetAxis(gripAxis);

        velocity = (transform.position - prevPos) / Time.deltaTime;
        angularVelocity = (transform.eulerAngles - prevAngRot);

        prevPos = transform.position;

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
