using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowWinner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform winnerPos;
    [SerializeField] Transform[] otherPlayersPos;
    [SerializeField] FloatReference maxScoreToWin;
    [SerializeField] FloatReference timeScale;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] GameObject fireworks;
    [SerializeField] float fireworksDelay;
    GameObject playerWinner;
    int pos;
    bool slowDown;
    private void Awake()
    {
        Time.timeScale = 1.0f;
        LookForGameWinner();
        PlaceOtherPlayers();
        StartCoroutine(EnableFireworks());
    }

    IEnumerator EnableFireworks()
    {
        yield return new WaitForSeconds(fireworksDelay);
        fireworks.SetActive(true);
    }

    private void Update()
    {
        if (slowDown)
        {
            Time.timeScale = timeScale.Value;
            timeScale.Value = 0.05f;
        }
        
    }

    private void LookForGameWinner()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].PlayerScore >= maxScoreToWin.Value)
            {
                playerWinner = Instantiate(playerPrefab, winnerPos.position, Quaternion.Euler(0,-90f,0));
                playerWinner.name = $"{playerJoinedData.GetPlayersJoined[i].name}_{playerJoinedData.GetPlayersJoined[i].CharacterName}";

                EnableCharacterModel(playerWinner, playerJoinedData.GetPlayersJoined[i].CharacterName.ToString());

                playerWinner.GetComponent<Rigidbody>().isKinematic = false;
                //playerWinner.GetComponent<Animator>().SetBool("Victory", true);
                StartCoroutine(VictoryPose());

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

                EnableCharacterModel(player, playerJoinedData.GetPlayersJoined[i].CharacterName.ToString());

                player.GetComponent<Rigidbody>().isKinematic = false;
                player.GetComponent<Animator>().SetBool("Dead", true);

                //enabling all scripts
                player.GetComponent<PlayerStatus>().EnableScripts(false);
                pos++;
            }
        }
    }

    IEnumerator VictoryPose()
    {
        yield return new WaitForSeconds(1.5f);
        if (playerWinner != null)
        {
            playerWinner.GetComponent<Animator>().SetTrigger("Chosen");
        }
    }

    public void ReduceTimeScale()
    {
      
        slowDown = true;
    }

    void EnableCharacterModel(GameObject player, string characterName)
    {
        player.transform.Find($"{characterName}_Head").gameObject.SetActive(true);
        player.transform.Find($"{characterName}_Body").gameObject.SetActive(true);
    }

}
