using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameModeSetUp))]
public class GameModeSetupEditor : Editor
{
    public VisualTreeAsset m_UXMl;
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        m_UXMl.CloneTree(root);

        var foldOut = new Foldout { viewDataKey = "Player Game Mode SetUp Full Inspector", text = "Data references" };
        InspectorElement.FillDefaultInspector(foldOut, serializedObject, this);
        root.Add(foldOut);
        return root;
    }
}
