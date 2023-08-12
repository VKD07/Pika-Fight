using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWinCameraEnabler : MonoBehaviour
{
    [SerializeField] GameObject playerWinCamera;

    public void EnableCamera()
    {
        playerWinCamera.SetActive(true);
    }
}
