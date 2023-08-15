using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices.ComTypes;

[CustomEditor(typeof(CreatingHandles))]
public class HandlesEditor : Editor
{
    float handleSize = 0.5f;
    CreatingHandles handles;
    private void OnEnable()
    {
        handles = target as CreatingHandles;
    }
    private void OnSceneGUI()
    {
        Draw();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AddPointBtn();
    }

    void Draw()
    {
        Handles.color = Color.red;
        if (handles.points.Count > 0)
        {
            for (int i = 0; i < handles.points.Count; i++)
            {
                Vector3 newPos = Handles.FreeMoveHandle(handles.points[i], handleSize, Vector3.zero, Handles.CylinderHandleCap);
                if (handles.points[i] != newPos)
                {
                    Undo.RecordObject(handles, "MovePoint");
                    handles.MovePoint(i, newPos);
                }
            }
        }
    }

    void AddPointBtn()
    {
        if (GUILayout.Button("Add Points"))
        {
            handles.points.Add(handles.points[handles.points.Count -1]);
        }
    }


}
