using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerSettings : MonoBehaviour
{
    [SerializeField] bool EnableRunTimeAdjustment;
    //Health and movement
    [HideInInspector] public float startingHealth = 100f;
    [HideInInspector] public float movementSpeed = 7f;
    //Dash
    [HideInInspector] public float dashForce = 20f;
    [HideInInspector] public float dashTime = 0.2f;
    [HideInInspector] public float dashCoolDown = 2f;
    //melee
    [HideInInspector] public float weaponDamage = 20f;
    [HideInInspector] public float attackRange = 1.2f;
    [HideInInspector] public float attackDashForce = 15f;
    [HideInInspector] public float attackDashTime = 0.1f;
    [HideInInspector] public float attackDashCoolDown = 0.5f;
    //DodgeBall
    [HideInInspector] public float ballMaxDamageForce = 60f;
    [HideInInspector] public float forceIncreaseRate = 60f;

    [Header("FloatReference ScriptableObjects")]
    [SerializeField] FloatReference playerStartingHealth;
    [SerializeField] FloatReference[] playermovementSpeed;

    [SerializeField] FloatReference playerdashForce;
    [SerializeField] FloatReference playerdashTime;
    [SerializeField] FloatReference playerdashCoolDown;

    [SerializeField] FloatReference playerweaponDamage;
    [SerializeField] FloatReference playerattackRange;
    [SerializeField] FloatReference playerattackDashForce;
    [SerializeField] FloatReference playerattackDashTime;
    [SerializeField] FloatReference playerattackDashCoolDown;

    [SerializeField] FloatReference playerballMaxDamageForce;
    [SerializeField] FloatReference playerforceIncreaseRate;

    private void Update()
    {
        if(EnableRunTimeAdjustment)
        {
            UpdatePlayerValues();
        }
    }
    public void UpdatePlayerValues()
    {
        playerStartingHealth.Value = startingHealth;

        foreach (var playerSpeed in playermovementSpeed)
        {
            playerSpeed.Value = movementSpeed;
        }

        playerdashForce.Value = dashForce;
        playerdashTime.Value = dashTime;
        playerdashCoolDown.Value = dashCoolDown;
        playerweaponDamage.Value = weaponDamage;
        playerattackRange.Value = attackRange;
        playerattackDashForce.Value = attackDashForce;
        playerattackDashTime.Value = attackDashTime;
        playerattackDashCoolDown.Value = attackDashCoolDown;
        playerballMaxDamageForce.Value = ballMaxDamageForce;
        playerforceIncreaseRate.Value = forceIncreaseRate;
    }
}
