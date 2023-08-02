using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlayerJoinedData))]
public class PlayerJoinedDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerJoinedData playerJoinedData = (PlayerJoinedData)target;

        base.OnInspectorGUI();

        ClearPlayerJoined(playerJoinedData);
    }

    private void ClearPlayerJoined(PlayerJoinedData pjd)
    {
        if(GUILayout.Button("Reset Data"))
        {
            for (int i = 0; i < pjd.GetPlayersJoined.Length; i++)
            {
                pjd.GetPlayersJoined[i] = null;
                pjd.GetPlayConfig[i].PlayerScore = 0;
                pjd.GetPlayConfig[i].Winner = false;
                pjd.GetPlayConfig[i].PlayerIsReady = false;
                pjd.GetPlayConfig[i].PlayerIsDead = false;
            }
        }
    }
}
