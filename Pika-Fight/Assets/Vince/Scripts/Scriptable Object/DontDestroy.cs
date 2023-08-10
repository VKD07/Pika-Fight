using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] PlayerConfig[] playerConfig;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] BoolReference versusScreen;
    [SerializeField] GameRandomizer gameRandomizer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        versusScreen.value = false;
    }
}
