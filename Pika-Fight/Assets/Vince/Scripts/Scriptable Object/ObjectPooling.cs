using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPooling", menuName = "Pattern/CharactersObjPooling")]
public class ObjectPooling : ScriptableObject
{
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] List<CharacterListContainer> listOfCharacterSlots;
    [SerializeField] List<Transform> parent;
    bool enableFirstModel;

    public void InitObjects()
    {
        for (int i = 0; i < parent.Count; i++)
        {
            for (int j = 0; j < gameObjects.Length; j++)
            {
                GameObject obj = Instantiate(gameObjects[j], Vector3.one, Quaternion.Euler(0f, 180f, 0f));
                obj.SetActive(false);
                listOfCharacterSlots[i].AddToList(obj);
                obj.transform.SetParent(parent[i]);
                obj.transform.position = parent[i].position;
            }
        }
    }

    public void AddParent(Transform parent)
    {
        this.parent.Add(parent);
    }

    public void ClearList()
    {
        parent.Clear();

        for (int i = 0; i < parent.Count; i++)
        {
            listOfCharacterSlots[i].ClearList();
        }
    }
    public List<Transform> GetCharactersSlot => parent;
    public List<CharacterListContainer> GetListOfCharacterSlots => listOfCharacterSlots;
}
