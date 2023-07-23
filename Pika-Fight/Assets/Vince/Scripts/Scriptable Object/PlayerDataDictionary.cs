using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataDictionary", menuName = "Player/PlayerDataDictionary")]
public class PlayerDataDictionary : ScriptableObject
{
    [NonReorderable]
    public List<KeyValuePair> MyList = new List<KeyValuePair>();
    public Dictionary<string, PlayerData> myDict = new Dictionary<string, PlayerData>();

    public void InitDictionary()
    {
        foreach (var kvp in MyList)
        {
            myDict[kvp.key] = kvp.playerData;
        }
    }
}

[Serializable]
public class KeyValuePair
{
    public string key;
    public PlayerData playerData;
}

