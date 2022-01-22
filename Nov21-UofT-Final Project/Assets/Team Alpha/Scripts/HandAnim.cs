using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandAnim : MonoBehaviour
{
    protected Animator animator;
    [SerializeField]
    private InputActionProperty flex;
    [SerializeField]
    private InputActionProperty point;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        animator.SetFloat("ControllerSelect", flex.action.ReadValue<float>());
        animator.SetFloat("ControllerActivate", point.action.ReadValue<float>());
    }
}
