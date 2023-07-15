using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerJoinedData", menuName = "Player/Player_Joined_Data")]
public class PlayerJoinedData : ScriptableObject
{
    [SerializeField] PlayerConfig[] playersJoined;
    [SerializeField] PlayerConfig[] playerConfig;
    public void AddPlayer(PlayerControls playerControls)
    {
        for (int i = 0; i < playersJoined.Length; i++)
        {
            if (playersJoined[i] == null)
            {
                playerConfig[i].Player_Controls = playerControls;
                playersJoined[i] = playerConfig[i];
                break;
            }
        }
    }

    public void ClearPlayers()
    {
        for (int i = 0; i < playersJoined.Length; i++)
        {
            playersJoined[i] = null;
        }
    }

    public PlayerConfig[] GetPlayersJoined => playersJoined; 
}
