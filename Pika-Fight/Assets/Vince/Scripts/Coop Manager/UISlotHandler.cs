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

    void Update()
    {
        DetectControllers();
        RemoveEmptyControllerNames();
        UpdateNumOfSlots();
        PlayerIsReady();
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
        if (connectedGamePads.Count > 0 && connectedGamePads.Count < 3)
        {
            slotsToJoin[2].SetActive(true);
        }
        else
        {
            slotsToJoin[2].SetActive(false);
        }

        if (connectedGamePads.Count > 1 && connectedGamePads.Count < 3)
        {
            slotsToJoin[3].SetActive(true);
        }
        else
        {
            slotsToJoin[3].SetActive(false);
        }
    }

    void PlayerIsReady()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                slotsToJoin[i].transform.Find("Join").gameObject.SetActive(false);
                slotsToJoin[i].transform.Find("Ready").gameObject.SetActive(true);
                UpdateReadyButton(i);
            }
        }
    }

    void UpdateReadyButton(int i)
    {
        var keyControl = playerJoinedData.GetPlayersJoined[i].Player_Controls.GetMovementAxes;
        string control = keyControl.ToString();
        print(control);

        if (keyControl.ToString() == "Joystick1" || keyControl.ToString() == "Joystick2")
        {
            readyTxt[i].SetText("A READY?");
        }
        else if (keyControl.ToString() == "WASD")
        {
            readyTxt[i].SetText("A READY?");
        }
        else if (keyControl.ToString() == "Arrow")
        {
            readyTxt[i].SetText($"{joinControls.Player_Arrow} READY?");
        }
    }
}
