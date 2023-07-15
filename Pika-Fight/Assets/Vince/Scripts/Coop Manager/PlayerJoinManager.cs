using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJoinManager : MonoBehaviour
{
    [Header("Keys to join the game")]
    [SerializeField] KeyCode Player_WASD = KeyCode.J;
    [SerializeField] KeyCode Player_Arrow = KeyCode.C;
    [SerializeField] KeyCode Player_Joystick1 = KeyCode.Joystick1Button0;
    [SerializeField] KeyCode Player_Joystick2 = KeyCode.Joystick2Button0;

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
        PlayerJoin(Player_WASD, 0);
        PlayerJoin(Player_Arrow, 1);
        PlayerJoin(Player_Joystick1, 2);
        PlayerJoin(Player_Joystick2, 3);
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