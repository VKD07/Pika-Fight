using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class KillTheChickenTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] GameObject timer;
    [SerializeField] TextMeshProUGUI timerValue;
    [SerializeField] float timerDuration = 60f;
    [SerializeField] UnityEvent OnTimerDone;
    float currentTime;

    [Header("Player Reference")]
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnWinnerFound;
    [SerializeField] UnityEvent OnEnableScript;

    float highestDamageDealt;
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
            playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken = 0;
        }
    }

    void StartTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            LookForHighestDamage();
            OnTimerDone.Invoke();
        }
    }

    private void UpdateTimerText()
    {
        timerValue.SetText(MathF.Round(currentTime, 2).ToString());
    }

    void LookForHighestDamage()
    {
        for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
        {
            if (playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken > highestDamageDealt && playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken > 0)
            {
                highestDamageDealt = playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken;
                playerIndex = i;
                playerJoinedData.GetPlayersJoined[playerIndex].Winner = true;
                OnWinnerFound.Invoke();
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Scene(sceneName));
    }

    IEnumerator Scene(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);

    }
}
