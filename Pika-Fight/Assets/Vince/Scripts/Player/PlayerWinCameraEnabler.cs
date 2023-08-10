using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCameraEnabler : MonoBehaviour
{
    [SerializeField] GameObject playerWinCamera;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            EnableCamera();
        }
    }
    public void EnableCamera()
    {
        playerWinCamera.SetActive(true);
    }
}
