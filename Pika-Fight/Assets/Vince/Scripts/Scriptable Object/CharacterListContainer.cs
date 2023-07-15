using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "CharacterListContainer", menuName = "CharacterListContainer")]
public class CharacterListContainer : ScriptableObject
{
    [SerializeField] List<GameObject> list;
    
    public void AddToList(GameObject item)
    {
        list.Add(item);
    }

    public void ClearList()
    {
        list.Clear();
    }

    public void DisableGameObjects()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(false);
        }
    }
    public List<GameObject> GetList => list;

    public void PickItem(int index)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(i == index)
            {
                list[i].SetActive(true);
            }
            else
            {
                list[i].SetActive(false);
            }
        }
    }
}
