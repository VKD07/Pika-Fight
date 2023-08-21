using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class KillTheChickenTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] float showTimerDelay = 3f;
    [SerializeField] Slider timer;
    [SerializeField] float timerDuration = 60f;
    [SerializeField] Gradient timerGradient;
    [SerializeField] Image fillImage;
    Animator timerAnim;
    float timeToStartAnim;
    [Space]
    [SerializeField] UnityEvent OnTimerDone;
    float currentTime;
    bool lookForHighDamage;

    [Header("Player Reference")]
    [SerializeField] PlayerJoinedData playerJoinedData;
    [SerializeField] UnityEvent OnWinnerFound;
    [SerializeField] UnityEvent OnEnableScript;

    float highestDamageDealt;
    int playerIndex;

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
        UpdateTimer();
        LookForHighestDamage();
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
            lookForHighDamage = true;
            currentTime = 0;
            OnTimerDone.Invoke();
        }
    }

    private void UpdateTimer()
    {
        timer.value = currentTime;
        fillImage.color = timerGradient.Evaluate(timer.normalizedValue);
        if (timer.value <= timeToStartAnim)
        {
            float speedValue = 1.0f - (currentTime / timerDuration);
            timerAnim.SetFloat("AnimationSpeed", speedValue * 2f);
        }
    }

    void LookForHighestDamage()
    {
        if (lookForHighDamage)
        {
            for (int i = 0; i < playerJoinedData.NumberOfPlayersJoined; i++)
            {
                if (playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken > highestDamageDealt && playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken > 0)
                {
                    highestDamageDealt = playerJoinedData.GetPlayersJoined[i].DamageDealtToChicken;
                    playerIndex = i;
                }
            }

            if (playerIndex != -1)
            {
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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);

    }
}
