using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizeGameMode : MonoBehaviour
{
    [SerializeField] GameRandomizer gameRandomizer;
    private void Awake()
    {
        gameRandomizer.RandomizeGameMode();
    }
}
