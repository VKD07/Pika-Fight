using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] FloatReference volumeSO;
    [SerializeField] Slider volumeSlider;
    [SerializeField] float startingValue = 0.7f;
    [SerializeField] float maxValue = 1f;
    [SerializeField] float minValue = 0f;

    private void Start()
    {
        volumeSlider.maxValue = maxValue;
        volumeSlider.value = volumeSO.Value;
    }
    void Update()
    {
        UpdateGameVolume();
    }

    private void UpdateGameVolume()
    {
        volumeSO.Value = volumeSlider.value;
        AudioListener.volume = volumeSO.Value;
    }
}
