using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBtn : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Sprite characterSprite;
    [SerializeField] string characterName;
    [SerializeField] bool characterIsTaken;

    public string GetCharacterName()
    {
        return characterPrefab.name;
    }

    public GameObject GetCharaterPrefab => characterPrefab;
    public Sprite CharacterSprite => characterSprite;
    public bool CharacterIsTaken
    {
        get { return characterIsTaken; }
        set { characterIsTaken = value; }
    }

    public string CharacterName { get => characterName; set => characterName = value; }
}
