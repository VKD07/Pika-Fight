using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGameMode : MonoBehaviour
{
    [SerializeField] GameRandomizer gameRandomizer;
    private void Awake()
    {
        gameRandomizer.ApplyGeneratedRandoms();
    }
}
