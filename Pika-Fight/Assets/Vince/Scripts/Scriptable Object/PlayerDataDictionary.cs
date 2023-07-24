using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataDictionary", menuName = "Player/PlayerDataDictionary")]
public class PlayerDataDictionary : ScriptableObject
{
    [NonReorderable]
    public List<DataDictionary> characterDatas = new List<DataDictionary>();
    public Dictionary<string, PlayerData> myDict = new Dictionary<string, PlayerData>();

    public void InitDictionary()
    {
        foreach (var kvp in characterDatas)
        {
            myDict[kvp.key] = kvp.playerData;
        }
    }
}

[Serializable]
public class DataDictionary
{
    public string key;
    public PlayerData playerData;
}

