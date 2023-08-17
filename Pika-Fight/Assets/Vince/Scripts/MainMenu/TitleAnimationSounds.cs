using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimationSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip swordClip;
    [SerializeField] AudioClip whoosh;
    [SerializeField] AudioClip whosh2;
    [SerializeField] float volume = 1.0f;

    public void SwordSound()
    {
        audioSource.PlayOneShot(swordClip, volume);
    }

    public void txtSound()
    {
        audioSource.PlayOneShot(whoosh, volume);
    }

    public void txt2Sound()
    {
        audioSource.PlayOneShot(whosh2, volume);
    }
}
