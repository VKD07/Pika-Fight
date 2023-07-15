using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBtn : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;

    public string GetCharacterName()
    {
        return characterPrefab.name;
    }

    public GameObject GetCharaterPrefab => characterPrefab;
}
