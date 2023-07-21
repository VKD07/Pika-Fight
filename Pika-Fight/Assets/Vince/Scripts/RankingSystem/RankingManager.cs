using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    [SerializeField] GameObject[] playersScorePanel;
    [SerializeField] PlayerJoinedData playerJoinedData;
    private void Awake()
    {
        DeactivateAllPanels();
        ActivateScorePanels();
    }
    void Start()
    {
        ResetDeath();
        SetPlayerScore();
        StartCoroutine(AddScore());
    }

    void DeactivateAllPanels()
    {
        //for (int i = 0; i < playersScorePanel.Length; i++)
        //{
        //    playersScorePanel[i].SetActive(false);
        //}
    }

    private void ActivateScorePanels()
    {
        for (int i = 0; i < playerJoinedData.GetNumberOfPlayersJoined(); i++)
        {
            playersScorePanel[i].SetActive(true);
        }
    }

    void ResetDeath()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].PlayerIsDead)
            {
                playerJoinedData.GetPlayersJoined[i].PlayerIsDead = false;
            }
        }
    }

    //Looking for player winner and adding score
    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(0);
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].Winner)
            {
                playerJoinedData.GetPlayersJoined[i].PlayerScore += 1;
                playerJoinedData.GetPlayersJoined[i].Winner = false;
                StartCoroutine(LoadScene());
                break;
            }
        }
        SetPlayerScore();
    }

    private void SetPlayerScore()
    {
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                playersScorePanel[i].GetComponent<PlayerScoreSheet>().SetScoreValue = playerJoinedData.GetPlayersJoined[i].PlayerScore;
            }
        }
    }

    //Load Back to Scene
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("VinceTest");
    }
}
