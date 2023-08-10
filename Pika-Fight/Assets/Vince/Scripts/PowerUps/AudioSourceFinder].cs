using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceFinder : MonoBehaviour
{
    AudioSource audioSource;
    private void OnEnable()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public AudioSource audioSourceAvailable
    {
        get { return audioSource; }
    }
}
