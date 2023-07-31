using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameRandomizer : ScriptableObject
{
    [Header("Combat Mode")]
    public GameMode[] combatEvents;
    public int randomCombat;

    [Header("Environmental Mode")]
    public GameMode[] environmentalEvents;
    public int randomEnvironmental;

    [Header("Game Goal")]
    public GameMode[] gameGoals;
    public int randomGoal;

    [Header("Scenes")]
    public string[] sceneNames;
    public int randomScenes;

    public void RandomizeGameMode()
    {
        randomCombat = Random.Range(0, combatEvents.Length);
        randomGoal = Random.Range(0, gameGoals.Length);
        randomScenes = Random.Range(0, sceneNames.Length);
    }

    public void ApplyGeneratedRandoms()
    {
        ResetModes();
        combatEvents[randomCombat].EnableMode();
        gameGoals[randomGoal].EnableMode();
    }

    void ResetModes()
    {
        for (int i = 0; i < combatEvents.Length; i++)
        {
            combatEvents[i].DisbleModes();
        }

        for (int i = 0; i < gameGoals.Length; i++)
        {
            gameGoals[i].DisbleModes();
        }
    }

    public void LoadRandomScene()
    {
         SceneManager.LoadScene(sceneNames[randomScenes]);
    }
}
