using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ObjectPool", menuName = "Pattern/ObjectPooling")]
public class ObjectPooling : ScriptableObject
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] List<GameObject> listOfObjs;
    [SerializeField] Transform parent;
    [SerializeField] float poolAmount;
    GameObject pickedObj;
    public void InitPoolOfObjects(Quaternion objRotation)
    {
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(objectToPool, Vector3.zero, objRotation);
            obj.transform.SetParent(parent);
            obj.SetActive(false);
            listOfObjs.Add(obj);
        }
    }

    public void PickObjFromPool(Transform locationToSpawn)
    {
        for (int i = 0; i <  listOfObjs.Count; i++)
        {
            if (!listOfObjs[i].activeSelf)
            {
                listOfObjs[i].transform.position = locationToSpawn.position;
                listOfObjs[i].SetActive(true);
                pickedObj = listOfObjs[i];
                break;
            }
        }
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
