using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum StartFinish
{
    Start,
    Finish
}

public class BeginningEnd : MonoBehaviour
{
    public StartFinish end = StartFinish.Start;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(end == StartFinish.Start)
            {
                Debug.Log("Hit starting");
            }
            else
            {
                Debug.Log("Hit end");
            }
        }
    }
}
