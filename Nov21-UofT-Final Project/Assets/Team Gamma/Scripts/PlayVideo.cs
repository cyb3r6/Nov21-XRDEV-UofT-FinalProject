using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class PlayVideo : MonoBehaviour
{
    public GameObject videoPlane;
    private VideoPlayer videoPlayer;
    private bool enable;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = videoPlane.GetComponent<VideoPlayer>();
        enable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayVideoOnPlane();
    }

    public void PlayVideoOnPlane()
    {
        enable = !enable;
        if (enable == false)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// Do something on End video
    /// </summary>
    private void OnEndVideo()
    {

    }
}