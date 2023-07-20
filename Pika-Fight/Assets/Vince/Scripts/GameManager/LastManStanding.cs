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
    [SerializeField] UnityEvent OnWinnerDeclared;
    [SerializeField] float numOfPlayers;
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
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
            OnWinnerDeclared.Invoke();
            GiveScoreToPlayer();
            Time.timeScale = 0.3f;
            StartCoroutine(DisableSlowMo());

        }
    }

    void GiveScoreToPlayer()
    {
        for(int i = 0; i < playerJoinedData.GetPlayersJoined.Length; i++)
        {
            if (!playerJoinedData.GetPlayersJoined[i].PlayerIsDead)
            {
                playerJoinedData.GetPlayersJoined[i].Winner = true;
                break;
            }
        }
    }

    IEnumerator DisableSlowMo()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
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
