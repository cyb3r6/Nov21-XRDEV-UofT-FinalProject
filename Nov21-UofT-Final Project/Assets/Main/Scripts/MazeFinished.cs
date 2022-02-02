using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFinished : MonoBehaviour
{
    public StartFinish end = StartFinish.Start;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            if (end == StartFinish.Start)
            {
                Debug.Log("Hit starting");
            }
            else
            {
                Debug.Log("Hit end");

                VRSceneManager.instance.ChangeScenes();

            }
        }
    }
    public void Finished()
    {
        VRSceneManager.instance.ChangeScenes();
    }
}
