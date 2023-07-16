using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_", menuName = "Player/Create_New_Player")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player Data")]
    [SerializeField] PlayerControls playerControl;
    [SerializeField] GameObject playerCharacter;
    [SerializeField] bool playerIsReady;

    public PlayerControls Player_Controls
    {
        set { playerControl = value; }
        get { return playerControl; }
    }

    public GameObject PlayerCharacter
    {
        get { return playerCharacter; }
        set { playerCharacter = value; }
    }

    public bool PlayerIsReady
    {
        get { return playerIsReady; }
        set { playerIsReady = value; }
    }

    private void OnDisable()
    {
        playerControl = null;
    }
}
