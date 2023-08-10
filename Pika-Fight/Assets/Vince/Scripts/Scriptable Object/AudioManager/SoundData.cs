using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData_", menuName = "Sound/SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip audioClip;
    public bool playOneShot;
    public bool looping;
    [Range(0, 1)]
    public float soundVolume = 0.5f;
}
