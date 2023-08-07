using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject selectCharacterTxt;
    [SerializeField] GameObject GameStartsInTxt;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnStart;

    private void Start()
    {
        playerJoinedData.NumberOfPlayersReady = 0;
        playerJoinedData.NumberOfPlayersJoined = 0;
    }

    void Update()
    {
        ShowStartBtn();
        playerJoinedData.GetNumOfPlayersReady();
        playerJoinedData.GetNumberOfPlayersJoined();
    }

    private void ShowStartBtn()
    {
        if (playerJoinedData.GetNumOfPlayersReady() > 1 && playerJoinedData.GetNumOfPlayersReady() >= playerJoinedData.GetNumberOfPlayersJoined())
        {
            selectCharacterTxt.SetActive(false);
            GameStartsInTxt.SetActive(true);
            countDown.SetActive(true);
        }
        else
        {
            selectCharacterTxt.SetActive(true);
            GameStartsInTxt.SetActive(false);
            countDown.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
