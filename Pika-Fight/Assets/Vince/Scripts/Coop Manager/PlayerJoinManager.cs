using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerJoinManager : MonoBehaviour
{
    [Header("Keys to join the game")]
    [SerializeField] JoinControls joinControls;

    [SerializeField] PlayerJoinedData playerJoinedData;

    [Header("PlayerControls")]
    [SerializeField] PlayerControls[] availableControls;

    [SerializeField] UnityEvent OnCharacterJoin;

    private void Awake()
    {
        ResetCoopData();
    }

    private void Update()
    {
        PlayerJoins();
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
            OnCharacterJoin.Invoke();
            availableControls[keyboardControlIndex].ControlIsTaken = true;
            playerJoinedData.AddPlayer(availableControls[keyboardControlIndex]);
        }
    }

    void ResetCoopData()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            playerJoinedData.GetPlayersJoined[i] = null;
            playerJoinedData.GetPlayConfig[i].PlayerScore = 0;
            playerJoinedData.GetPlayConfig[i].GemScore = 0;
            playerJoinedData.GetPlayConfig[i].Winner = false;
            playerJoinedData.GetPlayConfig[i].PlayerIsReady = false;
            playerJoinedData.GetPlayConfig[i].PlayerIsDead = false;
        }
    }

    //private void OnDisable()
    //{
    //    playerJoinedData.ClearPlayers();
    //}
}