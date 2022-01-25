using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Used for reading some input from the controller and controling hand visuals
/// </summary>
public class HandVisualController : MonoBehaviour
{
    [SerializeField]
    private string teleportationAnimationParameter;

    [SerializeField]
    private XRDirectInteractor directInteractor;

    public HandPrefab handPrefab;
    private ActionBasedController controller;
    private XRInteractorLineVisual lineVisual;
    private bool previouslyClicked = false;
    private bool lastFramEnabled = false;

    void Awake()
    {
        // get all the components
        controller = GetComponent<ActionBasedController>();
        lineVisual = GetComponent<XRInteractorLineVisual>();
        
        
        // set visuals to false
        lineVisual.enabled = false;

        controller.selectAction.action.started += Action_started;
        controller.selectAction.action.performed += Action_performed;
        controller.selectAction.action.canceled += Action_canceled;
    }

    private void Start()
    {
        
        //Debug.Log(handPrefab.gameObject.name,handPrefab.gameObject);
    }

    /// <summary>
    /// Fires when an action has been started
    /// </summary>
    /// <param name="obj"></param>
    private void Action_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //DebugManager.Instance.LogInfo("Select button is started");
        lineVisual.enabled = true;

        if (handPrefab == null)
        {
            handPrefab = directInteractor.GetComponentInChildren<HandPrefab>();
        }

        if (handPrefab != null)
        {
            handPrefab.Animator.SetBool(teleportationAnimationParameter, true);
        }
    }

    /// <summary>
    /// Fires when an action is fully performed.
    /// </summary>
    /// <param name="obj"></param>
    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //DebugManager.Instance.LogInfo("Select button is fully performed");
        lineVisual.enabled = true;
    }

    /// <summary>
    /// Fired when an action has been started but then canceled before
    /// </summary>
    /// <param name="obj"></param>
    private void Action_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //DebugManager.Instance.LogInfo("Select button is canceled");

        lineVisual.enabled = false;

        if (handPrefab == null)
        {
            handPrefab = directInteractor.GetComponentInChildren<HandPrefab>();
        }

        if (handPrefab != null)
        {
            handPrefab.Animator.SetBool(teleportationAnimationParameter, false);
        }
    }
}
