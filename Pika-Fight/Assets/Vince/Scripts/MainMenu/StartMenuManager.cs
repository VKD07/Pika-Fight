using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] float startDelay = 1f;
    [SerializeField] UnityEvent onAnyKeyPressed;
    bool menuEnabled;
    private void Awake()
    {
        StartCoroutine(EnableMenuManager());
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
}
