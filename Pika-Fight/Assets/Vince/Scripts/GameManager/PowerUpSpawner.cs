using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpsToSpawn;
    [SerializeField] ObjectsPooling objectsToPool;
    [SerializeField] public List<Vector3> spawnPoints;
    [SerializeField] public float minSpawnTime;
    [SerializeField] public float maxSpawnTime;
    int randomIndex;
    float randomSpawnTime;
    void Start()
    {
        AddPowerUpSpawnsToPool();
        objectsToPool.SetParent(transform);
        objectsToPool.InitPoolOfObjects(Quaternion.Euler(-90, 0, 0));
        StartCoroutine(StartSpawning());
    }

    void AddPowerUpSpawnsToPool()
    {
        foreach (GameObject obj in powerUpsToSpawn)
        {
            objectsToPool.ObjectsToPool.Add(obj);
        }
    }
    IEnumerator StartSpawning()
    {
        while (true)
        {
            randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            randomIndex = Random.Range(0, spawnPoints.Count);
            objectsToPool.PickObjFromPoolRandomly(spawnPoints[randomIndex]);
            CheckIfItsAGemPowerUP();
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    void CheckIfItsAGemPowerUP()
    {
        if (objectsToPool.GetPickedObj.GetComponent<Gem>() != null)
        {
            objectsToPool.GetPickedObj.GetComponent<Gem>().enabled = true;
        }
    }

    public void MoveSpawnPoint(int i, Vector3 pos)
    {
        spawnPoints[i] = pos;
    }

    private void OnDisable()
    {
        objectsToPool.ClearList();
    }
}
