using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] PlayerConfig[] playerConfig;
    [SerializeField] PlayerJoinedData playerJoinedData;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
