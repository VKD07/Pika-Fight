using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] float startDelay = 1f;
    [SerializeField] UnityEvent onAnyKeyPressed;
    bool menuEnabled;
    private void Awake()
    {
        StartCoroutine(EnableMenuManager());
        Time.timeScale = 1f;
    }

    IEnumerator EnableMenuManager()
    {
        yield return new WaitForSeconds(startDelay);
        menuEnabled = true;
    }

    private void Update()
    {
        CheckIfAnyKeyIsPressed();
    }

    private void CheckIfAnyKeyIsPressed()
    {
        if (menuEnabled && Input.anyKeyDown)
        {
            onAnyKeyPressed.Invoke();
            menuEnabled = false;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameWithDelay());
    }

    IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("PlayerJoin");
    }
}
