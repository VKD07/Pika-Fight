using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJoinManager : MonoBehaviour
{
    [Header("Keys to join the game")]
    [SerializeField] JoinControls joinControls;

    [SerializeField] PlayerJoinedData playerJoinedData;

    [Header("PlayerControls")]
    [SerializeField] PlayerControls[] availableControls;

    private void Update()
    {
        PlayerJoins();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("VinceTest");
        }
    }

    private void PlayerJoins()
    {
        PlayerJoin(joinControls.Player_WASD, 0);
        PlayerJoin(joinControls.Player_Arrow, 1);
        PlayerJoin(joinControls.Player_Joystick1, 2);
        PlayerJoin(joinControls.Player_Joystick2, 3);
    }

    void PlayerJoin(KeyCode key, int keyboardControlIndex)
    {
        if (Input.GetKeyDown(key) && !availableControls[keyboardControlIndex].ControlIsTaken)
        {
            availableControls[keyboardControlIndex].ControlIsTaken = true;
            playerJoinedData.AddPlayer(availableControls[keyboardControlIndex]);
        }
    }

    //private void OnDisable()
    //{
    //    playerJoinedData.ClearPlayers();
    //}
}