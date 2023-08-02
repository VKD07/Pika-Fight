using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stun : MonoBehaviour
{
    [SerializeField] float stunDuration = 2f;
    [SerializeField] PlayerAnimationData playerAnimData;
    [SerializeField] FloatReference playerHealth;
    [SerializeField] UnityEvent OnStunned;
    [SerializeField] UnityEvent UnStunned;

    bool playerIsStunned;

    private void OnEnable()
    {
        playerIsStunned = false;
    }

    private void Update()
    {
        TriggerStun();
    }

    private void TriggerStun()
    {
        if(playerIsStunned)
        {
            StartCoroutine(StunDelay()); // adding delay to avoid getting stunned and dying at thesame time
        }
    }
    
    IEnumerator StunDelay()
    {
        yield return new WaitForSeconds(0.1f);

        if (playerHealth.Value > 0)
        {
            OnStunned.Invoke();
            playerAnimData.IsStunned = true;
            StartCoroutine(DisableStun());
        }
    }

    IEnumerator DisableStun()
    {
        yield return new WaitForSeconds(stunDuration);
        UnStunned.Invoke();
        playerAnimData.IsStunned = false;
        playerIsStunned = false;
    }

    public PlayerAnimationData PlayerAnimationData { get => playerAnimData; set => playerAnimData = value; }
    public FloatReference PlayerHealth { get=> playerHealth; set => playerHealth = value; }
    public bool StunPlayer { get { return playerIsStunned; } set { playerIsStunned = value; } }
    
}
