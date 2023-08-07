using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReleaseTheBomb : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnableScript;


    private void OnEnable()
    {
        OnEnableScript.Invoke();
    }
}
