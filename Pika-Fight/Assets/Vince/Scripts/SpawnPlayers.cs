using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] Transform[] playerSpawners;
    [SerializeField] GameObject character;
    [SerializeField] int numberOfPlayers;
    void Start()
    {
        GetNumberOfPlayers();
        SpawnCharacters();
    }

    private void GetNumberOfPlayers()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                numberOfPlayers++;
            }
        }
    }

    void SpawnCharacters()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            int randomPos = Random.Range(0, playerSpawners.Length);

           GameObject player = Instantiate(character, playerSpawners[randomPos].position, Quaternion.identity);
           player.GetComponent<PlayerMovement>().SetPlayerControls = playerJoinedData.GetPlayersJoined[i].Player_Controls;
        }
    }
}
