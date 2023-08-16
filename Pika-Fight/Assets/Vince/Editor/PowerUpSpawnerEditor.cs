using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(PowerUpSpawner))]
public class PowerUpSpawnerEditor : Editor
{
    public VisualTreeAsset m_UXMl;
    Button addPointsBtn;
    Button removePointsBtn;
    PowerUpSpawner powerUpSpawner;
    float handleSize = 1f;
    VisualElement root;


    public override VisualElement CreateInspectorGUI()
    {
        powerUpSpawner = (PowerUpSpawner)target;
        root = new VisualElement();
        m_UXMl.CloneTree(root);

        var foldOut = new Foldout { viewDataKey = "Power Up Spawner Full Inspector Foldout", text = "Data references" };
        InspectorElement.FillDefaultInspector(foldOut, serializedObject, this);
        root.Add(foldOut);
        RegisterUI(root);

        return root;
    }

    void RegisterUI(VisualElement root)
    {
        addPointsBtn = root.Q<Button>("AddPoints");
        addPointsBtn.clicked += AddSpawnPointButton;
        removePointsBtn = root.Q<Button>("RemovePoint");
        removePointsBtn.clicked += RemoveAPointButton;
    }

    private void OnSceneGUI()
    {
        DrawMoveHandlers();
    }

    void DrawMoveHandlers()
    {
        Handles.color = Color.blue;
        if (powerUpSpawner.spawnPoints.Count > 0)
        {
            for (int i = 0; i < powerUpSpawner.spawnPoints.Count; i++)
            {
                Vector3 newPos = Handles.FreeMoveHandle(powerUpSpawner.spawnPoints[i], handleSize, Vector3.zero, Handles.CylinderHandleCap);
                if (powerUpSpawner.spawnPoints[i] != newPos)
                {
                    Undo.RecordObject(powerUpSpawner, "MovePoint");
                    powerUpSpawner.MoveSpawnPoint(i, newPos);
                }
            }
        }
    }

    public void AddSpawnPointButton()
    {
        if (powerUpSpawner.spawnPoints.Count > 0)
        {
            powerUpSpawner.spawnPoints.Add(powerUpSpawner.spawnPoints[powerUpSpawner.spawnPoints.Count - 1]);
        }
        else
        {
            powerUpSpawner.spawnPoints.Add(Vector3.one * 2f);
        }

    }

    void RemoveAPointButton()
    {
        powerUpSpawner.spawnPoints.Remove(powerUpSpawner.spawnPoints[powerUpSpawner.spawnPoints.Count - 1]);
    }

    void RemoveAllSpawnPointsBtn()
    {
        if (GUILayout.Button("Remove All SpawnPoints"))
        {
            powerUpSpawner.spawnPoints.Clear();
        }
    }
}
