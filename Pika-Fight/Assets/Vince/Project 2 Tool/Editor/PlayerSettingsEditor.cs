using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(PlayerSettings))]
public class PlayerSettingsEditor : Editor
{
    public VisualTreeAsset m_UXMl;
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        m_UXMl.CloneTree(root);

        var foldOut = new Foldout { viewDataKey = "Player Settings Full Inspector Foldout", text = "Data references" };
        InspectorElement.FillDefaultInspector(foldOut, serializedObject, this);
        root.Add(foldOut);
        return root;
    }
}
