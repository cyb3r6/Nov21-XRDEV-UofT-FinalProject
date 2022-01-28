using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour
{
    public UnityEvent onbutton;

    private void OnCollisionEnter(Collision collision)
    {
        onbutton?.Invoke();
    }
}
