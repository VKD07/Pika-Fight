using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LastManStanding : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] FloatReference numOfPlayersDied;
    [SerializeField] UnityEvent OnWinnerFound;
    [SerializeField] UnityEvent OnWinnerDeclared;
    float numOfPlayers;

    void Start()
    {
        numOfPlayers = 0;
        numOfPlayers = playerJoinedData.GetNumberOfPlayersJoined();
    }

    void Update()
    {
        DeclareWinner();
    }

    private void DeclareWinner()
    {
        if(numOfPlayersDied.Value >= numOfPlayers -1)
        {
            StartCoroutine(Invoke());
            StartCoroutine(OnWinnerDeclare());
            GiveScoreToPlayer();
        }
    }


    void GiveScoreToPlayer()
    {
        for(int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i] != null && !playerJoinedData.GetPlayersJoined[i].PlayerIsDead)
            {
                playerJoinedData.GetPlayersJoined[i].Winner = true;
                break;
            }
        }
    }

    IEnumerator Invoke()
    {
        yield return null;
        OnWinnerFound.Invoke();

    }

    IEnumerator OnWinnerDeclare()
    {
        yield return new WaitForSeconds(2f);
        OnWinnerDeclared.Invoke();
    }


    private void OnDisable()
    {
        numOfPlayersDied.Value = 0;
    }

    //Loading Score scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneDelay(sceneName));
    }

    public IEnumerator LoadSceneDelay(string sceneName)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName);
    }
}
