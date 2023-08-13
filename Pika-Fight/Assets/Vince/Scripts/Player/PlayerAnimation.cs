using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] FloatReference velocity;
    [SerializeField] PlayerAnimationData playerAnimData;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetBool("Alive", false);
        anim.SetBool("Alive", true);
    }


    private void Update()
    {
        RunAnim();
        BallThrowAnim();
        BallOnHand();
        Stunned();
    }

    private void BallOnHand()
    {
        anim.SetBool("BallOnHand", playerAnimData.BallOnHand);
    }

    public void RunAnim()
    {
        anim.SetFloat("Velocity", velocity.Value);
    }
    public void BallThrowAnim()
    {
        anim.SetBool("Throw", playerAnimData.IsThrowing);
    }

    void Stunned()
    {
        anim.SetBool("Stunned", playerAnimData.IsStunned);
    }

    private void OnDisable()
    {
        playerAnimData.BallOnHand = false;
        anim.SetBool("Alive", false);
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
    public FloatReference PlayerVelocity { set => velocity = value; }
    public PlayerAnimationData PlayerAnimData { set => playerAnimData = value; }
}
