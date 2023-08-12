using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSound : MonoBehaviour
{
    [SerializeField] AudioClip beepSound;
    [SerializeField] float beepVolume = 0.5f;
    Animator anim;
    public Bomb bomb;
    AudioSource audioSource;
    void Start()
    {
        anim = GetComponent<Animator>();
        bomb = GetComponentInParent<Bomb>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("AnimSpeed", bomb.CurrentTime * 0.5f);
    }

    public void PlayerBeepSound()
    {
        audioSource.PlayOneShot(beepSound, beepVolume);
    }
}
