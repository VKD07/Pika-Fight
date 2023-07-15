using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersHandler : MonoBehaviour
{
    [SerializeField] ObjectPooling pooling;
    [SerializeField] Transform[] characterModelsSlots;
    void Awake()
    {
        for (int i = 0; i < characterModelsSlots.Length; i++)
        {
            pooling.AddParent(characterModelsSlots[i]);
        }
    }
    private void Start()
    {
        pooling.InitObjects();
    }

    private void OnDisable()
    {
        for (int i = 0; i < pooling.GetListOfCharacterSlots.Count; i++)
        {
            pooling.GetListOfCharacterSlots[i].ClearList();
        }

        pooling.ClearList();
    }
}
