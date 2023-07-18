using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] FloatReference playerHealth;
    [SerializeField] UnityEvent OnDeath;

    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if(playerHealth.Value <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
