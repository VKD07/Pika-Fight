using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlotHandler : MonoBehaviour
{
    [SerializeField] GameObject[] slotsToJoin;
    [SerializeField] List<string> connectedGamePads;
    [SerializeField] PlayerJoinedData playerJoinedData;
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
            }
        }
    }
}
