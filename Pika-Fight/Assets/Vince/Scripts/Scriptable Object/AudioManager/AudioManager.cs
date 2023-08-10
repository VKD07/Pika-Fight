using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManager", menuName = ("Sound/AudioManager"))]
public class AudioManager : ScriptableObject
{
    public SoundData[] sounds;
    AudioSource audioSource;
    public void SetAudioSource(AudioSource source)
    {
        audioSource = source;
    }
    public void PlaySound(string name)
    {
        SoundData sound = GetSoundData(name);

        if (sound != null)
        {
            if (sound.playOneShot)
            {
                audioSource.PlayOneShot(sound.audioClip);
            }
            else
            {
                audioSource.clip = sound.audioClip;
                audioSource.loop = sound.looping;
                audioSource.Play();
            }
            audioSource.volume = sound.soundVolume;
        }
    }

    public void FindAudioSource()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    SoundData GetSoundData(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (soundName.Contains(sounds[i].name))
            {
                return sounds[i];
            }
        }
        return null;
    }
}
