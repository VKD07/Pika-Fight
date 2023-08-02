using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeListAdder : MonoBehaviour
{
    [SerializeField] GameModeListDrawer[] gameModeList;

    private void Awake()
    {
        AddToList();
    }

    private void AddToList()
    {
        for (int i = 0; i < gameModeList.Length; i++)
        {
            for (int j = 0; j < gameModeList[i].listOfScriptsToEnable.Length; j++)
            {
                gameModeList[i].gameMode.Add(gameModeList[i].listOfScriptsToEnable[j]);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < gameModeList.Length; i++)
        {
            gameModeList[i].gameMode.ClearList();
        }
    }
}

