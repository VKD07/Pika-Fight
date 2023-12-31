using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GemHunt : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] Slider timer;
    [SerializeField] float showTimerDelay = 3f;
    [SerializeField] float timerDuration = 60f;
    [SerializeField] Gradient timerGradient;
    [SerializeField] Image fillImage;
    Animator timerAnim;
    float timeToStartAnim;
    [SerializeField] UnityEvent OnTimerDone;
    float currentTime;
    bool timerDone;

    [Header("Player Reference")]
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnWinnerFound;
    [SerializeField] UnityEvent OnEnableScript;

    float highestGemScore = -1;
    int playerIndex = -1;

    private void OnEnable()
    {
        StartCoroutine(ShowTimer());
        InitTimer();
        OnEnableScript.Invoke();
        ResetGemScore();
    }

    private void InitTimer()
    {
        timerAnim = timer.gameObject.GetComponent<Animator>();
        timeToStartAnim = timerDuration / 2;
        timer.maxValue = timerDuration - 5;
        timer.value = timerDuration;
        fillImage.color = timerGradient.Evaluate(1f);
        currentTime = timerDuration;
    }

    IEnumerator ShowTimer()
    {
        yield return new WaitForSeconds(showTimerDelay);
        timer.gameObject.SetActive(true);
    }

    void Update()
    {
        StartTimer();
        UpdateTimerText();
        LookForHighestGemScore();
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
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            timerDone = true;
            StartCoroutine(OnWinnerDeclare());
        }
    }

    private void UpdateTimerText()
    {
        timer.value = currentTime;
        fillImage.color = timerGradient.Evaluate(timer.normalizedValue);
        if (timer.value <= timeToStartAnim)
        {
            float speedValue = 1.0f - (currentTime / timerDuration);
            timerAnim.SetFloat("AnimationSpeed", speedValue * 2f);
        }
    }

    void LookForHighestGemScore()
    {
        if (timerDone)
        {
            for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
            {
                if (playerJoinedData.GetPlayersJoined[i].GemScore > highestGemScore && playerJoinedData.GetPlayersJoined[i].GemScore > 0)
                {
                    highestGemScore = playerJoinedData.GetPlayersJoined[i].GemScore;
                    playerIndex = i;
                }
            }

            if (playerIndex != -1)
            {
                List<int> playersWithHighestScore = new List<int>();
                for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
                {
                    if (playerJoinedData.GetPlayersJoined[i].GemScore == highestGemScore && i != playerIndex)
                    {
                        playersWithHighestScore.Add(i);
                    }
                }

                if (playersWithHighestScore.Count == 0)
                {
                    playerJoinedData.GetPlayersJoined[playerIndex].Winner = true;
                    OnWinnerFound.Invoke();
                }
            }
        }
    }

    IEnumerator OnWinnerDeclare()
    {
        yield return new WaitForSeconds(2f);
        OnTimerDone.Invoke();
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
