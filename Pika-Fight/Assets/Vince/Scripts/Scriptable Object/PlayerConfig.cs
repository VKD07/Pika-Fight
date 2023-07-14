using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_", menuName = "Player/Create_New_Player")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] PlayerControls playerControl;

    public PlayerControls Player_Controls
    {
        set { playerControl = value; }
        get { return playerControl; }
    }


    private void OnDisable()
    {
        playerControl = null; 
    }
}
