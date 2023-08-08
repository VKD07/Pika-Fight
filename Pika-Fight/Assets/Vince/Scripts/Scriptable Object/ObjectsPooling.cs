using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsPool", menuName = "Pattern/ObjectsPooling")]
public class ObjectsPooling : ScriptableObject
{
    [SerializeField] GameObject[] objectsToPool;
    [SerializeField] List<GameObject> listOfObjs;
    [SerializeField] Transform parent;
    [SerializeField] float poolAmount;
    GameObject pickedObj;
    public void InitPoolOfObjects(Quaternion objRotation)
    {
        for (int i = 0; i < poolAmount; i++)
        {
            for (int j = 0; j < objectsToPool.Length; j++)
            {
                GameObject obj = Instantiate(objectsToPool[i], Vector3.zero, objRotation);
                obj.transform.SetParent(parent);
                obj.SetActive(false);
                listOfObjs.Add(obj);
            }
        }
    }

    public void PickObjFromPoolRandomly(Transform locationToSpawn)
    {
        int randomIndex = Random.Range(0, listOfObjs.Count);
        while (listOfObjs[randomIndex].activeSelf)
        {
            randomIndex = Random.Range(0, listOfObjs.Count);
        }
        listOfObjs[randomIndex].transform.position = locationToSpawn.position;
        listOfObjs[randomIndex].SetActive(true);
        pickedObj = listOfObjs[randomIndex];
    }

    public void SetParent(Transform parent)
    {
        this.parent = parent;
    }

    public void ClearList()
    {
        listOfObjs.Clear();
    }

    public GameObject GetPickedObj { get { return pickedObj; } }
}
