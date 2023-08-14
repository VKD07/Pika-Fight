using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardBindings : MonoBehaviour
{
    [SerializeField] string textShownWhenPressed = "Press Any Key";
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] AbilityName abilityName;
    [SerializeField] KeyCode keyCode;
    bool startBinding;

    private void Awake()
    {
        ApplyCurrentControlSettingsToText();
    }

    private void ApplyCurrentControlSettingsToText()
    {
        switch (abilityName)
        {
            case AbilityName.Attack:

                text.SetText(playerControls.GetAttackKey.ToString());

                break;

            case AbilityName.Dash:
                text.SetText(playerControls.GetDashKey.ToString());
                break;
        }
    }

    private void Update()
    {
        if (startBinding)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        keyCode = key;
                        SetAbilityKey(key);
                        text.SetText(keyCode.ToString());
                        startBinding = false;
                        break;
                    }
                }
            }
        }
    }

    public void EnableKeyBinding()
    {
        text.SetText(textShownWhenPressed);
        startBinding = true;
    }

    void SetAbilityKey(KeyCode key)
    {
        switch (abilityName)
        {
            case AbilityName.Attack:

                playerControls.GetAttackKey = key;

                break;

            case AbilityName.Dash:
                playerControls.GetDashKey = key;
                break;
        }
    }

    public enum AbilityName
    {
        Attack,
        Dash
    }
}
