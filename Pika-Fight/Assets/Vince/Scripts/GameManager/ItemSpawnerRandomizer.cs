using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerRandomizer : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;
    [SerializeField] Transform poolParent;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    int randomIndex;
    float randomSpawnTime;
    void Start()
    {
        objectPool.SetParent(poolParent);
        objectPool.InitPoolOfObjects(Quaternion.Euler(-90,0,0));
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
            randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomSpawnTime);
            randomIndex = Random.Range(0,spawnPoints.Count);
            objectPool.PickObjFromPool(spawnPoints[randomIndex]);
        }
    }

    public void AddToList(GameObject point)
    {
        spawnPoints.Add(point.transform);
    }

    public void ClearList()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            DestroyImmediate(spawnPoints[i]);
        }
        spawnPoints.Clear();
    }

    private void OnDisable()
    {
      //  objectPool.ClearList();
    }
}
