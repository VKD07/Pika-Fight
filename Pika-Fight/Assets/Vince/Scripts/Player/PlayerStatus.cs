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
    [SerializeField] List<MonoBehaviour> playerSciptsEnabled;
    private void Awake()
    {
        playerScripts = GetComponents<MonoBehaviour>();
    }

    private void Start()
    {
        GetScriptsThatAreEnabled();
    }

    private void GetScriptsThatAreEnabled()
    {
        foreach (var script in playerScripts)
        {
            if (script.enabled)
            {
                playerSciptsEnabled.Add(script);
            }
        }
    }

    public void ReEnableScriptsAfterRespawning()
    {
        for (int i = 0; i < playerSciptsEnabled.Count; i++)
        {
            playerSciptsEnabled[i].enabled = true;
        }
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
