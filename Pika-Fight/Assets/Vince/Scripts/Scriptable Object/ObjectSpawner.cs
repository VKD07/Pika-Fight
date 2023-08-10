using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ObjectSpawner", menuName = "ObjectSpawner")]
public class ObjectSpawner : ScriptableObject
{
    [SerializeField] GameObject objToSpawn;
    [SerializeField] float timeToDestroy;

    public void InstantiateObj(Transform spawnLocation, Quaternion objRotation)
    {
        GameObject obj = Instantiate(objToSpawn, spawnLocation.position, objRotation);
        Destroy(obj, timeToDestroy);
    }
}
