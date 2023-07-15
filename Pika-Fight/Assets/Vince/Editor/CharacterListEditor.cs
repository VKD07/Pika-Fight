using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System;

[CustomEditor(typeof(CharacterListContainer))]
public class CharacterListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CharacterListContainer characterListContainer = (CharacterListContainer)target;
        base.OnInspectorGUI();
        ClearList(characterListContainer);
    }

    private void ClearList(CharacterListContainer characterListContainer)
    {
        if(GUILayout.Button("Clear List"))
        {
            characterListContainer.ClearList();
        }
    }
}
