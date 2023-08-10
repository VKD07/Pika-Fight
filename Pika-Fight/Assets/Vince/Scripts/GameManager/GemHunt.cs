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

    [Header("Player Reference")]
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnWinnerFound;
    [SerializeField] UnityEvent OnEnableScript;

    float highestGemScore;
    int playerIndex;

    private void OnEnable()
    {
        StartCoroutine(ShowTimer());
        timerAnim = timer.gameObject.GetComponent<Animator>();
        timeToStartAnim = timerDuration / 2;
        timer.maxValue = timerDuration - 5;
        timer.value = timerDuration;
        fillImage.color = timerGradient.Evaluate(1f);
        currentTime = timerDuration;
        OnEnableScript.Invoke();
        ResetGemScore();
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
            LookForHighestGemScore();
            OnWinnerFound.Invoke();
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
