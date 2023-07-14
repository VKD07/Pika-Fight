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

    [Header("Slots")]
    [SerializeField] GameObject[] slots;
    [SerializeField] PlayerJoinedData playerJoinedData;

    [Header("PlayerControls")]
    [SerializeField] PlayerControls[] availableControls;

    [Header("Connected GamePads")]
    [SerializeField] List<string> connectedGamePads;


    private void Update()
    {
        PlayerJoins();
        DetectControllers();
        RemoveEmptyControllerNames();
        UpdateSlots();

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
    private void DetectControllers()
    {
        connectedGamePads = new List<string>(Input.GetJoystickNames());
    }

    void RemoveEmptyControllerNames()
    {
        connectedGamePads.RemoveAll(name => string.IsNullOrEmpty(name));
    }

    void UpdateSlots()
    {
        if (connectedGamePads.Count > 0 && connectedGamePads.Count < 3)
        {
            slots[2].SetActive(true);
        }
        else
        {
            slots[2].SetActive(false);
        }

        if (connectedGamePads.Count > 1 && connectedGamePads.Count < 3)
        {
            slots[3].SetActive(true);
        }
        else
        {
            slots[3].SetActive(false);
        }
    }

    //private void OnDisable()
    //{
    //    playerJoinedData.ClearPlayers();
    //}
}