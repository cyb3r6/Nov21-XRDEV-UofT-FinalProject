using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTone : MonoBehaviour
{
    public AudioClip tone;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    public void PlaySound()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(tone);
        }
    }
}
