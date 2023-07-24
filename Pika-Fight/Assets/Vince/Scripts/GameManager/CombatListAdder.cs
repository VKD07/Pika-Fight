using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatListAdder : MonoBehaviour
{
    [SerializeField] CombatListDrawer[] combatList;

    private void Awake()
    {
        AddToList();
    }

    private void AddToList()
    {
        for (int i = 0; i < combatList.Length; i++)
        {
            for (int j = 0; j < combatList[i].listOfScriptsToEnable.Length; j++)
            {
                combatList[i].combatEvent.Add(combatList[i].listOfScriptsToEnable[j]);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < combatList.Length; i++)
        {
            combatList[i].combatEvent.ClearList();
        }
    }
}

