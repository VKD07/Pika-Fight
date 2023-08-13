using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationSoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    [SerializeField] float soundVolume = 0.6f;
    public void PlaySound()
    {
        audioSource.PlayOneShot(clip,soundVolume);
    }
}
