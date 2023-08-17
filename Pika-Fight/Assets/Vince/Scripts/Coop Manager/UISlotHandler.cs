using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISlotHandler : MonoBehaviour
{
    [SerializeField] GameObject[] slotsToJoin;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] JoinControls joinControls;
    [SerializeField] TextMeshProUGUI[] readyTxt;
    [SerializeField] List<string> connectedGamePads;
    bool vfxPlayed;

    void Update()
    {
        DetectControllers();
        RemoveEmptyControllerNames();
        //UpdateNumOfSlots();
        PlayerHasJoined();
        // Unready();
    }

    private void DetectControllers()
    {
        connectedGamePads = new List<string>(Input.GetJoystickNames());
    }

    void RemoveEmptyControllerNames()
    {
        connectedGamePads.RemoveAll(name => string.IsNullOrEmpty(name));
    }

    void UpdateNumOfSlots()
    {
        if (connectedGamePads.Count > 0 && connectedGamePads.Count < 5)
        {
            slotsToJoin[2].SetActive(true);
        }
        else
        {
            slotsToJoin[2].SetActive(false);
        }

        if (connectedGamePads.Count > 1 && connectedGamePads.Count < 5)
        {
            slotsToJoin[3].SetActive(true);
        }
        else
        {
            slotsToJoin[3].SetActive(false);
        }
    }

    void PlayerHasJoined()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                var keyControl = playerJoinedData.GetPlayersJoined[i].Player_Controls.GetMovementAxes;
                string control = keyControl.ToString();

                slotsToJoin[i].transform.Find("Join").gameObject.SetActive(false);
                slotsToJoin[i].transform.Find("Ready").gameObject.SetActive(true);
                slotsToJoin[i].transform.Find("Cancel").gameObject.SetActive(false);
                if (!playerJoinedData.GetPlayersJoined[i].PlayerIsReady)
                {
                    slotsToJoin[i].transform.Find("CharacterJoinVfx").gameObject.SetActive(false);
                    ShowReadyButton(i, control);
                }
                else
                {
                    PlayerHasChosenACharacter(i, control);
                }
            }
        }
    }

    void ShowReadyButton(int i, string control)
    {
        if (control == "Joystick1" || control == "Joystick2" || control == "Joystick3")
        {
            readyTxt[i].SetText("A READY?");
        }
        else if (control == "WASD")
        {
            readyTxt[i].SetText("J READY?");
        }
        else if (control == "Arrow")
        {
            readyTxt[i].SetText($"{joinControls.Player_Arrow} READY?");
        }
    }

    void PlayerHasChosenACharacter(int i, string control)
    {
        if (playerJoinedData.GetPlayersJoined[i].PlayerIsReady)
        {
            if (control == "Joystick1" || control == "Joystick2" || control == "Joystick3")
            {
                readyTxt[i].SetText("B to Cancel");
            }
            else if (control == "WASD")
            {
                readyTxt[i].SetText("Esc to Cancel");
            }
            else if (control == "Arrow")
            {
                readyTxt[i].SetText("Q to Cancel");
            }

            slotsToJoin[i].transform.Find("Ready").gameObject.SetActive(false);
            slotsToJoin[i].transform.Find("Cancel").gameObject.SetActive(true);
            slotsToJoin[i].transform.Find("CharacterJoinVfx").gameObject.SetActive(true);
        }
    }
}
