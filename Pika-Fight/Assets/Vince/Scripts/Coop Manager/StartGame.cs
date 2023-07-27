using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject startGameBtn;
    [SerializeField] Slider startSlider;
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnStart;

    private void Start()
    {
        startGameBtn.SetActive(false);
        playerJoinedData.NumberOfPlayersReady = 0;
        playerJoinedData.NumberOfPlayersJoined = 0;
        startSlider.value = 0;
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
            startGameBtn.SetActive(true);
            StartTheGame();
        }
        else
        {
            startSlider.value = 0;
            startGameBtn.SetActive(false);
        }
    }

    void StartTheGame()
    {
        if (Input.GetKey(playerJoinedData.GetPlayersJoined[0].Player_Controls.PlayerStartKey))
        {
            startSlider.value += 0.5f * Time.deltaTime;

            if (startSlider.value >= 1)
            {
                OnStart.Invoke();
            }
        }
        else
        {
            startSlider.value = 0;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
