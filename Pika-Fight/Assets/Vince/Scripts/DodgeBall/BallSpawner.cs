using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform dodgeBallsParent;
    [SerializeField] GameRandomizer gameRandomizer;
    [SerializeField] List<GameObject> ballsSpawned;
    public GameObject GetBall => ball;
    public Transform GetSpawnPoint => spawnPoint;

    private void Start()
    {
        if(gameRandomizer.randomCombat == 0)
        {
            dodgeBallsParent.gameObject.SetActive(true);
        }
        else
        {
            dodgeBallsParent.gameObject.SetActive(false);
        }
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
