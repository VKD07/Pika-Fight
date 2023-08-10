using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "GameMode/Create_New_Mode")]
public class GameMode : ScriptableObject
{
    public List<MonoBehaviour> scriptToEnable;

    public void Add(MonoBehaviour script)
    {
        scriptToEnable.Add(script);
    }

    public void Remove(MonoBehaviour script)
    {
        scriptToEnable.Remove(script);
    }

    public void ClearList()
    {
        scriptToEnable.Clear();
    }

    public void EnableMode()
    {
        scriptToEnable.RemoveAll(script => script == null);

        for (int i = 0; i < scriptToEnable.Count; i++)
        {
            scriptToEnable[i].enabled = true;
        }
    }

    public void DisbleModes()
    {
        scriptToEnable.RemoveAll(script => script == null);
        for (int i = 0; i < scriptToEnable.Count; i++)
        {
            scriptToEnable[i].enabled = false;
        }
    }
}
