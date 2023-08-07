using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CaptureAndDefend : MonoBehaviour
{
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] float percentageValueToWin = 100f;
    [SerializeField] UnityEvent OnEnableScript;
    [SerializeField] UnityEvent OnWinnerFound;
    [SerializeField] UnityEvent OnWinnerDeclared;

    private void OnEnable()
    {
        OnEnableScript.Invoke();
    }

    private void Start()
    {
        playerJoinedData.GetNumberOfPlayersJoined();
        ResetPlayerHoldPercentage();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinner();
    }

    private void ResetPlayerHoldPercentage()
    {
        for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
        {
            playerJoinedData.GetPlayersJoined[i].HoldPercentage = 0;
        }
    }

    private void CheckWinner()
    {
        for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
        {
            if (playerJoinedData.GetPlayConfig[i].HoldPercentage >= percentageValueToWin)
            {
                playerJoinedData.GetPlayConfig[i].Winner = true;
                OnWinnerFound.Invoke();
                StartCoroutine(OnWinnerDeclare());
                break;
            }
        }
    }

    IEnumerator OnWinnerDeclare()
    {
        yield return new WaitForSeconds(2f);
        OnWinnerDeclared.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
