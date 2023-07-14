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
        if(GUILayout.Button("Clear Player Joined"))
        {
            for (int i = 0; i < pjd.GetPlayersJoined.Length; i++)
            {
                pjd.GetPlayersJoined[i] = null;
            }
        }
    }
}
