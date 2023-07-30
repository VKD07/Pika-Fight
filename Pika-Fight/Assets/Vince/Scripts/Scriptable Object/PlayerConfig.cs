using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Player_", menuName = "Player/Create_New_PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Data")]
    [SerializeField] PlayerControls playerControl;
    [SerializeField] Sprite characterSprite;
    [SerializeField] string characterName;
    [SerializeField] int playerOverallScore;
    [SerializeField] float gemScore;
    [SerializeField] float holdPercentage;
    [SerializeField] bool winner;
    [SerializeField] bool playerIsReady;
    [SerializeField] bool playerIsDead;

    public PlayerControls Player_Controls
    {
        set { playerControl = value; }
        get { return playerControl; }
    }

    public Sprite CharacterSprite { get => characterSprite; set => characterSprite = value; }

    public string CharacterName { get => characterName; set => characterName = value; } 

    public int PlayerScore { get => playerOverallScore; set => playerOverallScore = value; }

    public float GemScore { get => gemScore; set => gemScore = value; }

    public float HoldPercentage { get => holdPercentage; set => holdPercentage = value; }

    public bool Winner { get => winner; set => winner = value; }
    public bool PlayerIsReady
    {
        get { return playerIsReady; }
        set { playerIsReady = value; }
    }

    public bool PlayerIsDead { get => playerIsDead; set => playerIsDead = value; }

    private void OnDisable()
    {
        playerControl = null;
    }
}
