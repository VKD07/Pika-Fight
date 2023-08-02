using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenMode : MonoBehaviour
{
    [SerializeField] GameObject chickenForm;
    [SerializeField] FloatReference movementSpeed;
    [SerializeField] float chickenModeMovementSpeed;
    [SerializeField] UnityEvent OnChickenForm;
    [SerializeField] UnityEvent OnNormalForm;
    [SerializeField] PlayerConfigBridge playerConfigBridge;
    [SerializeField] FloatReference velocity;
    [SerializeField] Animator chickenAnimation;
    GameObject head;
    GameObject body;
    float initMovementSpeed;
    private void OnEnable()
    {
        playerConfigBridge = GetComponentInParent<PlayerConfigBridge>();
        initMovementSpeed = movementSpeed.Value;
        NormalForm(false);
        ChickenForm(true);
    }

    private void OnDisable()
    {
        NormalForm(true);
        ChickenForm(false);
    }

    private void Update()
    {
        ChickenFormAnim();
    }

    private void ChickenFormAnim()
    {
        chickenAnimation.SetFloat("Velocity", velocity.Value);
    }

    void NormalForm(bool enable)
    {
        if (enable == true)
        {
            OnNormalForm.Invoke();
        }
        transform.parent.Find($"{playerConfigBridge.PlayerConfig.CharacterName}_Head").gameObject.SetActive(enable);
        transform.parent.Find($"{playerConfigBridge.PlayerConfig.CharacterName}_Body").gameObject.SetActive(enable);
    }

    void ChickenForm(bool enable)
    {
        if (enable == true)
        {
            OnChickenForm.Invoke();
            movementSpeed.Value = chickenModeMovementSpeed;
        }
        else
        {
            movementSpeed.Value = initMovementSpeed;
        }
        chickenForm.SetActive(enable);
    }

    public FloatReference PlayerMovementSpeed { set => movementSpeed = value; }
    public FloatReference PlayerVelocity { set => velocity = value; }
}
