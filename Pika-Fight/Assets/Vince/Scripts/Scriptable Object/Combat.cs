using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEvent_", menuName = "CombatEvent/Combat")]
public class Combat : ScriptableObject
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
        for (int i = 0; i < scriptToEnable.Count; i++)
        {
            scriptToEnable[i].enabled = true;
        }
    }

    public void DisbleModes()
    {
        for (int i = 0; i < scriptToEnable.Count; i++)
        {
            scriptToEnable[i].enabled = false;
        }
    }
}
