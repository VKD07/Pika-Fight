using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControls;

[CreateAssetMenu(fileName = "PlayerJoinedData", menuName = "Player/Player_Joined_Data")]
public class PlayerJoinedData : ScriptableObject
{
    [SerializeField] PlayerConfig[] playersJoined;
    [SerializeField] PlayerConfig[] playerConfig;
    float numOfPlayersJoined;
    float numOfPlayersReady;

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

    public float GetNumberOfPlayersJoined()
    {
        numOfPlayersJoined = 0;
        for (int i = 0; i < playersJoined.Length; i++)
        {
            if (playersJoined[i] != null)
            {
                numOfPlayersJoined++;
            }
            else
            {
                return numOfPlayersJoined;
            }
        }

        return 0;
    }

    public float GetNumOfPlayersReady()
    {
        numOfPlayersReady = 0;

        for (int i = 0; i < playersJoined.Length; i++)
        {
            if (playersJoined[i] != null && playersJoined[i].PlayerIsReady)
            {
                numOfPlayersReady++;
            }
            else
            {
                return numOfPlayersReady;
            }
        }
        return 0;
    }

    public PlayerConfig[] GetPlayersJoined => playersJoined;
    public PlayerConfig[] GetPlayConfig => playerConfig;
    public float NumberOfPlayersJoined { get => numOfPlayersJoined; set => numOfPlayersJoined = value; }
    public float NumberOfPlayersReady { get => numOfPlayersReady; set => numOfPlayersReady = value; }
}
