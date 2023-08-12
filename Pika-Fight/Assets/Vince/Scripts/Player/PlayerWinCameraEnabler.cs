using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCameraEnabler : MonoBehaviour
{
    [SerializeField] GameObject playerWinCamera;
    public void EnableCamera()
    {
        playerWinCamera.SetActive(true);
    }
}
