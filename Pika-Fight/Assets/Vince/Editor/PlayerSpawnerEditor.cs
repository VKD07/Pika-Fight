using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(SpawnPlayers))]
public class PlayerSpawnerEditor : Editor
{
    private Button addPointsBtn;
    private Button removePointsBtn;
    public VisualTreeAsset m_UXMl;
    SpawnPlayers playerSpawner;
    float handleSize = 0.5f;

    public override VisualElement CreateInspectorGUI()
    {
        playerSpawner = (SpawnPlayers)target;
        var root = new VisualElement();
        m_UXMl.CloneTree(root);

        var foldOut = new Foldout { viewDataKey = "Player Spawner Full Inspector Foldout", text = "Data references" };
        InspectorElement.FillDefaultInspector(foldOut, serializedObject, this);
        root.Add(foldOut);

        RegisterButtons(root);

        return root;
    }

    void RegisterButtons(VisualElement root)
    {
        addPointsBtn = root.Q<Button>("AddPoints");
        addPointsBtn.clicked += AddPlayerSpawnPointButton;
        removePointsBtn = root.Q<Button>("RemovePoint");
        removePointsBtn.clicked += RemoveAPointButton;

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

    public void AddPlayerSpawnPointButton()
    {
        if (playerSpawner.SpawnPoints.Count < 4)
        {
            if (playerSpawner.SpawnPoints.Count > 0)
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
        playerSpawner.SpawnPoints.Remove(playerSpawner.SpawnPoints[playerSpawner.SpawnPoints.Count - 1]);
    }

    void RemoveAllSpawnPointsBtn()
    {
        if (GUILayout.Button("Remove All SpawnPoints"))
        {
            playerSpawner.SpawnPoints.Clear();
        }
    }
}
