using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameRandomizer : ScriptableObject
{
    public Combat[] combatEvents;
    public int randomCombat;

    public void RandomizeGameMode()
    {
        randomCombat = Random.Range(0, combatEvents.Length);
    }

    public void ApplyGeneratedRandoms()
    {
        ResetModes();
        combatEvents[randomCombat].EnableMode();
    }

    void ResetModes()
    {
        for (int i = 0; i < combatEvents.Length; i++)
        {
            combatEvents[i].DisbleModes();
        }
    }
}
