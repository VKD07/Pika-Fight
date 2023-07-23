using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPlayers))]
public class PlayerSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SpawnPlayers spawnPlayers = (SpawnPlayers)target;
        base.OnInspectorGUI();
        AddPlayerSpawnPointButton(spawnPlayers);
        RemoveAllSpawnPointsBtn(spawnPlayers);
    }

    void AddPlayerSpawnPointButton(SpawnPlayers spawnPlayers)
    {
        if(GUILayout.Button("Add Spawn Point"))
        {
            GameObject playerSpawnPoint = new GameObject("PlayerSpawnPoint");
            playerSpawnPoint.transform.SetParent(spawnPlayers.playerSpawnerParent);
            playerSpawnPoint.transform.position = spawnPlayers.playerSpawnerParent.position;
            playerSpawnPoint.transform.rotation = Quaternion.identity;
            spawnPlayers.AddToSpawnList(playerSpawnPoint.transform);
        }
    }

    void RemoveAllSpawnPointsBtn(SpawnPlayers spawnPlayers)
    {
        if (GUILayout.Button("Remove All SpawnPoints"))
        {
            for (int i = spawnPlayers.PlayerSpawners.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(spawnPlayers.PlayerSpawners[i].gameObject);
            }
            spawnPlayers.PlayerSpawners.Clear();
        }
    }
}
