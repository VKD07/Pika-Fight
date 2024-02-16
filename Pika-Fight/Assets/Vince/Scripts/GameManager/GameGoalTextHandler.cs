using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameGoalTextHandler : MonoBehaviour
{
    [SerializeField] GameRandomizer gameRandomizer;
    [SerializeField] string[] gameGoals;
    public TextMeshProUGUI goalDescription => GetComponent<TextMeshProUGUI>();

    void Start()
    {
        ShowTheRightGoalDescription();
    }

    void ShowTheRightGoalDescription()
    {
        int randomGoalIndex = gameRandomizer.randomGoal;
        if (randomGoalIndex >= 0 && randomGoalIndex < gameGoals.Length)
        {
            goalDescription.SetText(gameGoals[randomGoalIndex]);
        }
    }
}
