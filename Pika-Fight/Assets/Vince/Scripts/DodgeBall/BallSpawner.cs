using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallSpawner : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.Find("DodgeBalls").SetActive(true);
    }
}
