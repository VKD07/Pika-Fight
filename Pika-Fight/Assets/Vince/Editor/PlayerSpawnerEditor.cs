using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPlayers))]
public class PlayerSpawnerEditor : Editor
{
    SpawnPlayers playerSpawner;
    float handleSize = 0.5f;
    public override void OnInspectorGUI()
    {
        playerSpawner = (SpawnPlayers)target;
        base.OnInspectorGUI();
        AddPlayerSpawnPointButton();
        RemoveAPointButton();
        RemoveAllSpawnPointsBtn();
    }

    private void OnSceneGUI()
    {
        DrawMoveHandlers();
    }

    void DrawMoveHandlers()
    {
        Handles.color = Color.red;
        if (playerSpawner.SpawnPoints.Count > 0)
        {
            for (int i = 0; i < playerSpawner.SpawnPoints.Count; i++)
            {
                Vector3 newPos = Handles.FreeMoveHandle(playerSpawner.SpawnPoints[i], handleSize, Vector3.zero, Handles.CylinderHandleCap);
                if (playerSpawner.SpawnPoints[i] != newPos)
                {
                    Undo.RecordObject(playerSpawner, "MovePoint");
                    playerSpawner.MoveSpawnPoint(i, newPos);
                }
            }
        }
    }

    void AddPlayerSpawnPointButton()
    {
        if(GUILayout.Button("Add Spawn Point"))
        {
            if(playerSpawner.SpawnPoints.Count > 0)
            {
                playerSpawner.SpawnPoints.Add(playerSpawner.SpawnPoints[playerSpawner.SpawnPoints.Count - 1]);
            }
            else
            {
                playerSpawner.SpawnPoints.Add(Vector3.one * 2f);
            }
        }
    }

    void RemoveAPointButton()
    {
        if (GUILayout.Button("Remove Previous SpawnPoint"))
        {
            playerSpawner.SpawnPoints.Remove(playerSpawner.SpawnPoints[playerSpawner.SpawnPoints.Count - 1]);
        }
    }

    void RemoveAllSpawnPointsBtn()
    {
        if (GUILayout.Button("Remove All SpawnPoints"))
        {
            playerSpawner.SpawnPoints.Clear();
        }
    }
}
