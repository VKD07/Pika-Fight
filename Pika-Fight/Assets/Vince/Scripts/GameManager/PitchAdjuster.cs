using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PitchAdjuster : MonoBehaviour
{
    [SerializeField] Slider gameTimer;
    [SerializeField] float halfTimePitchValue = 1.5f;
    [SerializeField] float quaterTimePitchValue = 2f;
    AudioSource audioSource;
    bool halfTimeDone;
    public float halfTime;
    public float quarterTime;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        halfTime = gameTimer.maxValue / 2;
        quarterTime = halfTime / 2;
    }
    private void Update()
    {
        IncreasePitch();
    }

    private void IncreasePitch()
    {
        if (gameTimer.gameObject.activeSelf)
        {
            float targetPitch = audioSource.pitch;

            if (gameTimer.value <= quarterTime)
            {
                targetPitch = quaterTimePitchValue;
            }else if (gameTimer.value <= halfTime && gameTimer.value > quarterTime)
            {
                targetPitch = halfTimePitchValue;
            }
           

            float pitchSpeed = 0.1f;
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, targetPitch, pitchSpeed * Time.deltaTime);
        }
    }
}
