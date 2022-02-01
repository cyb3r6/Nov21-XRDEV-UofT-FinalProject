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
    [SerializeField]
    private InputActionProperty thumbs;

    private float flexnumber1;

    private float thumbnumber;

    private float pointnumber;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        animator.SetFloat("ControllerSelect", flex.action.ReadValue<float>());
        animator.SetFloat("ControllerActive", point.action.ReadValue<float>());
        animator.SetFloat("ControllerActivate", thumbs.action.ReadValue<float>());
        flexnumber1 = flex.action.ReadValue<float>();
        thumbnumber = thumbs.action.ReadValue<float>();
        pointnumber = point.action.ReadValue<float>();
        if(flexnumber1 >= .85f && thumbnumber >= .85f && pointnumber >= .85f)
        {
            animator.Play("Fist_Pose");
        }
    }
}
