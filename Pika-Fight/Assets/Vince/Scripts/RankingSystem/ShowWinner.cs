using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWinner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform winnerPos;
    [SerializeField] Transform[] otherPlayersPos;
    [SerializeField] FloatReference maxScoreToWin;
    [SerializeField] PlayerJoinedData playerJoinedData;
    int pos;
    private void Awake()
    {
        Time.timeScale = 1.0f;
        LookForGameWinner();
        PlaceOtherPlayers();
    }

    private void LookForGameWinner()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].PlayerScore >= maxScoreToWin.Value)
            {
                GameObject playerWinner = Instantiate(playerPrefab, winnerPos.position, Quaternion.Euler(0,-90f,0));
                playerWinner.name = $"{playerJoinedData.GetPlayersJoined[i].name}_{playerJoinedData.GetPlayersJoined[i].CharacterName}";

                EnableCharacterModel(playerWinner, playerJoinedData.GetPlayersJoined[i].CharacterName);

                playerWinner.GetComponent<Rigidbody>().isKinematic = false;
                playerWinner.GetComponent<Animator>().SetBool("Victory", true);

                //enabling all scripts
                playerWinner.GetComponent<PlayerStatus>().EnableScripts(false);
                break;
            }
        }
    }

    void PlaceOtherPlayers()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].PlayerScore < maxScoreToWin.Value)
            {
                GameObject player = Instantiate(playerPrefab, otherPlayersPos[pos].position, Quaternion.Euler(0, -90f, 0));
                player.name = $"{playerJoinedData.GetPlayersJoined[i].name}_{playerJoinedData.GetPlayersJoined[i].CharacterName}";

                EnableCharacterModel(player, playerJoinedData.GetPlayersJoined[i].CharacterName);

                player.GetComponent<Rigidbody>().isKinematic = false;
                player.GetComponent<Animator>().SetBool("Dead", true);

                //enabling all scripts
                player.GetComponent<PlayerStatus>().EnableScripts(false);
                pos++;
            }
        }
    }

    void EnableCharacterModel(GameObject player, string characterName)
    {
        player.transform.Find($"{characterName}_Head").gameObject.SetActive(true);
        player.transform.Find($"{characterName}_Body").gameObject.SetActive(true);
    }

}
