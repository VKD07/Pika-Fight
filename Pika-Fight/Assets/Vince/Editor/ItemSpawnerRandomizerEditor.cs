using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemSpawnerRandomizer))]
public class ItemSpawnerRandomizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemSpawnerRandomizer itemSpawnerRandomizer = (ItemSpawnerRandomizer)target;
        base.OnInspectorGUI();
        CreateASpawnPoint(itemSpawnerRandomizer);
        RemoveAllSpawnPoints(itemSpawnerRandomizer);

    }

    private static void CreateASpawnPoint(ItemSpawnerRandomizer itemSpawnerRandomizer)
    {
        if (GUILayout.Button("Spawn A SpawnPoint"))
        {
            GameObject spawnPoint = new GameObject();
            spawnPoint.name = "spawnPoint (1)";


            itemSpawnerRandomizer.AddToList(spawnPoint);
        }
    }

    void RemoveAllSpawnPoints(ItemSpawnerRandomizer itemSpawnerRandomizer)
    {
        if (GUILayout.Button("Remove All SpawnPoints"))
        {
            itemSpawnerRandomizer.ClearList();
        }
    }
}
