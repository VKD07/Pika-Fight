using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSetUp : MonoBehaviour
{
    public CombatTypes combatTypes;
    public GameGoals gameGoals;
    public Environments environments;
    public bool applyGamemodes;
    public GameRandomizer gameRandomizer;
    private void Awake()
    {
        if (applyGamemodes)
        {
            gameRandomizer.randomCombat = (int)combatTypes;
            gameRandomizer.randomGoal = (int)gameGoals;
            gameRandomizer.randomScenes = (int)environments;
        }
    }
}
