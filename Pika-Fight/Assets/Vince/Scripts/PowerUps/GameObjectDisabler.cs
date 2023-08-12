using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDisabler : MonoBehaviour
{
    [SerializeField] float timeToDisable = 10f;
    [SerializeField] bool deactivate;
    float currentTime;
    public bool runTimer;
  

    private void OnEnable()
    {
        runTimer = true;
    }

    private void Update()
    {
        if (!deactivate)
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
