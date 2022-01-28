using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class RayToggler : MonoBehaviour
{
    [SerializeField]
    private InputActionReference activateReference = null;

    private XRRayInteractor rayInteractor = null;
    private bool isEnabled = false;

    
    void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    private void OnEnable()
    {
        activateReference.action.started += ToggleRay;
        activateReference.action.canceled += ToggleRay;
    }


    private void OnDisable()
    {
        activateReference.action.started -= ToggleRay;
        activateReference.action.canceled -= ToggleRay;
    }

    /// <summary>
    /// Once we release the joystick and try to teleport
    /// This will disable the interactor and we won't actually teleport
    /// </summary>
    /// <param name="context"></param>
    private void ToggleRay(InputAction.CallbackContext context)
    {
        isEnabled = context.control.IsPressed();
    }

    void LateUpdate()
    {
        ApplyStatus();
    }

    private void ApplyStatus()
    {
        // if the ray interactor enabled difers from what we're currently
        // getting from our input, then we want to apply it
        if(rayInteractor.enabled != isEnabled)
        {
            rayInteractor.enabled = isEnabled;
        }
    }
}
