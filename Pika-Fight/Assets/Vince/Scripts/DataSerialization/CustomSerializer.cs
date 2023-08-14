using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomSerializer
{
    public static string Serialize(DataToSave data)
    {
        return $"|Volume:{data.gameVolume}|" +
       $"\nKeyboard1 Attackkey:{data.Keyboard1AttackKey}|" +
       $"\nKeyboard1 DashKey:{data.Keyboard1DashKey}|" +
       $"\nKeyboard2 AttackKey:{data.Keyboard2AttackKey}|" +
       $"\nKeyboard2 DashKey:{data.Keyboard2DashKey}";
    }

    public static DataToSave Deserialize(string data)
    {
        string[] parts = data.Split('|');
        if (parts.Length >= 6)
        {
            DataToSave dataToSave = new DataToSave();
            float.TryParse(parts[1].Split(':')[1], out dataToSave.gameVolume);
            dataToSave.Keyboard1AttackKey = parts[2].Split(':')[1];
            dataToSave.Keyboard1DashKey = parts[3].Split(':')[1];
            dataToSave.Keyboard2AttackKey = parts[4].Split(':')[1];
            dataToSave.Keyboard2DashKey = parts[5].Split(':')[1];
            return dataToSave;
        }
        return null;
    }
}
