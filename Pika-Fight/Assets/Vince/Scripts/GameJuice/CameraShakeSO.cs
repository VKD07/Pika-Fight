using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="CameraShake_", menuName = "GameJuice/CameraShake")]
public class CameraShakeSO : ScriptableObject
{
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;

    public void TriggerShake()
    {
        ShakeCamera.instance.TriggerShake(shakeIntensity, shakeTime);
    }
}
