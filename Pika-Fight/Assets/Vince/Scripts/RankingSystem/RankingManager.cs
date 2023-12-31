using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] GameObject[] playersScorePanel;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] FloatReference maxScoreToWin;
    [SerializeField] float timeToLoad = 4f;
    [SerializeField] UnityEvent OnFinishScoring;
    [SerializeField] UnityEvent OnDeclareWinner;
    [SerializeField] UnityEvent WinnerSfx;
    bool declareWinner;
    private void Awake()
    {
        DeactivateAllPanels();
        ActivateScorePanels();
        SetPlayerCharacterImages();
    }
    void Start()
    {
        ResetDeath();
        SetPlayerScore();
        //DeclareOverallWinner();
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

    void SetPlayerCharacterImages()
    {
        for (int i = 0; i < playerJoinedData.GetNumberOfPlayersJoined(); i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null)
            {
                playersScorePanel[i].transform.Find("PlayerImage").Find("Image").GetComponent<Image>().sprite = playerJoinedData.GetPlayersJoined[i].CharacterSprite;
            }
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
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].Winner)
            {
                playerJoinedData.GetPlayersJoined[i].PlayerScore += 1;
                playerJoinedData.GetPlayersJoined[i].Winner = false;

                //check if there is already player who reach the max score
                if (playerJoinedData.GetPlayersJoined[i].PlayerScore >= maxScoreToWin.Value)
                {
                    declareWinner = true;
                    WinnerSfx.Invoke();
                    StartCoroutine(LoadToRanking());
                    break;
                }
                else
                {
                    StartCoroutine(LoadBackToGameScene());
                    break;
                }
            }
            else
            {
                StartCoroutine(LoadBackToGameScene()); //tie
            }
        }
        SetPlayerScore();
    }

    //void DeclareOverallWinner()
    //{
    //    for (int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
    //    {
    //        if (playerJoinedData.GetPlayersJoined[i] != null && playerJoinedData.GetPlayersJoined[i].PlayerScore >= maxScoreToWin.Value)
    //        {
    //            declareWinner = true;
    //            StartCoroutine(LoadToRanking());
    //            break;
    //        }
    //    }
    //}

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
    IEnumerator LoadBackToGameScene()
    {
        yield return new WaitForSeconds(timeToLoad);

        OnFinishScoring.Invoke();
    }

    IEnumerator LoadToRanking()
    {
        yield return new WaitForSeconds(timeToLoad - 1);
        OnDeclareWinner.Invoke();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
