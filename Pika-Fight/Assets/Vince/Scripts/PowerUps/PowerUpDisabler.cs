using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerUpDisabler : MonoBehaviour
{
    [SerializeField] float timeToDisable = 10f;
    [SerializeField] bool deactivate;
    public float currentTime;
    bool runTimer;
    Ball ball;
    private void Start()
    {
        ball = GetComponent<Ball>();
    }
    private void OnEnable()
    {
        runTimer = true;
    }

    private void Update()
    {
        if (ball != null && !ball.BallTaken && !deactivate)
        {
            Timer();
        }
        else
        {
            currentTime = 0;
        }
    }

    private void Timer()
    {
        if (runTimer && currentTime < timeToDisable)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            runTimer = false;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        currentTime = 0;
        runTimer = false;
    }
}
