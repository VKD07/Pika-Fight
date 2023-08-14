using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVolumeUpdater : MonoBehaviour
{
    [SerializeField] FloatReference gameVolume;
    private void Update()
    {
        AudioListener.volume = gameVolume.Value;
    }
}
