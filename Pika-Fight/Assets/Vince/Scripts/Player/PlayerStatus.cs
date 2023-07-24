using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] FloatReference playerHealth;
    [SerializeField] UnityEvent OnDeath;
    MonoBehaviour[] playerScripts;
    private void Awake()
    {
        playerScripts = GetComponents<MonoBehaviour>();
    }

    public void EnableScripts(bool enable)
    {
        for (int i = 0; i < playerScripts.Length; i++)
        {
            playerScripts[i].enabled = enable;
        }
    }

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
    public FloatReference PlayerHealth { set { playerHealth = value; } }
}
