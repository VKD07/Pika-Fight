using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform dodgeBallsParent;
    [SerializeField] GameRandomizer gameRandomizer;
    [SerializeField] List<GameObject> ballsSpawned;
    [SerializeField] UnityEvent OnEnableScript;
    public GameObject GetBall => ball;
    public Transform GetSpawnPoint => spawnPoint;

    private void OnEnable()
    {
        OnEnableScript.Invoke();
    }

    public void AddToList(GameObject ball)
    {
        ballsSpawned.Add(ball);
    }

    public void ClearList()
    {
        for (int i = 0; i < ballsSpawned.Count; i++)
        {
            DestroyImmediate(ballsSpawned[i]);
        }

        ballsSpawned.Clear();
    }
}
