using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GemHunt : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] GameObject timer;
    [SerializeField] TextMeshProUGUI timerValue;
    [SerializeField] float timerDuration = 60f;
    [SerializeField] UnityEvent OnTimerDone;
    float currentTime;

    [Header("Player Reference")]
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnEnableScript;

    float highestGemScore;
    int playerIndex;

    private void OnEnable()
    {
        timer.SetActive(true);
        currentTime = timerDuration;
        OnEnableScript.Invoke();
        ResetGemScore();
    }

    void Update()
    {
        StartTimer();
        UpdateTimerText();
    }

    void ResetGemScore()
    {
        for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
        {
            playerJoinedData.GetPlayersJoined[i].GemScore = 0;
        }
    }

    void StartTimer()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            LookForHighestGemScore();
            OnTimerDone.Invoke();
        }
    }

    private void UpdateTimerText()
    {
        timerValue.SetText(MathF.Round(currentTime,2).ToString());
    }

    void LookForHighestGemScore()
    {
        for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i].GemScore > highestGemScore && playerJoinedData.GetPlayersJoined[i].GemScore > 0)
            {
                highestGemScore = playerJoinedData.GetPlayersJoined[i].GemScore;
                playerIndex = i;
                playerJoinedData.GetPlayersJoined[playerIndex].Winner = true;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Scene(sceneName));
    }

    IEnumerator Scene(string sceneName)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);

    }
}
