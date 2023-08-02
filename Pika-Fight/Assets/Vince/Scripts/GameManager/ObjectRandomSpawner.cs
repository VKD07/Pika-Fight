using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform objectParent;
    [SerializeField] GameRandomizer gameRandomizer;
    [SerializeField] List<GameObject> ballsSpawned;
    public GameObject ObjectToSpawn => objectToSpawn;
    public Transform GetSpawnPoint => spawnPoint;

    private void Start()
    {
        
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
