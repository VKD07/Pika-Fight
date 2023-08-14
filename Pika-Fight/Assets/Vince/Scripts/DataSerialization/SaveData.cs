using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SaveData : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] FloatReference gameVolume;
    [SerializeField] PlayerControls wasdControl;
    [SerializeField] PlayerControls arrowControl;
    [SerializeField] string fileName = "GameSettings";

    private void Awake()
    {
        
    }

    public void SaveNewData()
    {
        DataToSave newData = new DataToSave();
        newData.gameVolume = gameVolume.Value;
        newData.Keyboard1AttackKey = wasdControl.GetAttackKey.ToString();
        newData.Keyboard1DashKey = wasdControl.GetDashKey.ToString();
        newData.Keyboard2AttackKey = arrowControl.GetAttackKey.ToString();
        newData.Keyboard2DashKey = arrowControl.GetDashKey.ToString();
        SaveDataAndWrite(newData);
    }

    void SaveDataAndWrite(DataToSave data)
    {
        string newData = CustomSerializer.Serialize(data);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.txt", newData);
    }
}
