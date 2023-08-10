using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] ObjectsPooling objectsToPool;
    [SerializeField] Transform poolParent;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    int randomIndex;
    float randomSpawnTime;
    void Start()
    {
        objectsToPool.SetParent(poolParent);
        objectsToPool.InitPoolOfObjects(Quaternion.Euler(-90, 0, 0));
        StartCoroutine(StartSpawning());
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
            //StartCoroutine(DisableObj());
        }
    }

    void CheckIfItsAGemPowerUP()
    {
        if (objectsToPool.GetPickedObj.GetComponent<Gem>()!= null)
        {
            objectsToPool.GetPickedObj.GetComponent<Gem>().enabled = true;
        }
    }

    //IEnumerator DisableObj()
    //{
    //    yield return new WaitForSeconds(disableAfterSpawnDuration);

    //    Ball ball = objectsToPool.GetPickedObj.GetComponent<Ball>();
    //    ExplodingCrate explodingCrate = objectsToPool.GetPickedObj.GetComponent<ExplodingCrate>();

    //    if (ball != null && !ball.BallTaken)
    //    {
    //        objectsToPool.GetPickedObj.SetActive(false);
    //    }
    //    else if (explodingCrate != null)
    //    {
    //        //Nothing
    //    }
    //    else
    //    {
    //        objectsToPool.GetPickedObj.SetActive(false);
    //    }
    //}

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
        //objectPool.ClearList();
    }
}
