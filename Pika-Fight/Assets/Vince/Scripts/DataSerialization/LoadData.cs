using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] FloatReference gameVolume;
    [SerializeField] PlayerControls wasdControl;
    [SerializeField] PlayerControls arrowControl;
    [SerializeField] string fileName = "GameSettings";

    private void Start()
    {
        LoadAndApplyData();
    }
    void LoadAndApplyData()
    {
        try
        {
            string savedData = File.ReadAllText(Application.persistentDataPath + $"/{fileName}.txt");

            if (savedData != null)
            {
                DataToSave deserializedData = CustomSerializer.Deserialize(savedData);
                gameVolume.Value = deserializedData.gameVolume;
                wasdControl.GetAttackKey = ParseKeyCode(deserializedData.Keyboard1AttackKey);
                wasdControl.GetDashKey = ParseKeyCode(deserializedData.Keyboard1DashKey);
                arrowControl.GetAttackKey = ParseKeyCode(deserializedData.Keyboard2AttackKey);
                arrowControl.GetDashKey = ParseKeyCode(deserializedData.Keyboard2DashKey);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.LogWarning("File not found. Using default values.");
        }
    }

    KeyCode ParseKeyCode(string keyString)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), keyString);
    }
}
