using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeBall : MonoBehaviour
{

    [Header("Ball Settings")]
    [SerializeField] float ballForce = 50f;
    [SerializeField] float maxForce = 100f;
    [SerializeField] float forceIncreaseRate = 20f;

    [Header("DodgeBall Reference")]
    [SerializeField] GameObject ball;
    [SerializeField] bool ballOnHand;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] Transform ballPlaceHolder;
    [SerializeField] FloatReference velocity;
    [SerializeField] FloatReference playerMovementSpeed;
    float initMovementSpeed;

    [Header("Direction UI")]
    [SerializeField] GameObject directionBar;
    [SerializeField] Slider directionFillBar;

    [Header("Player Animation")]
    [SerializeField] PlayerAnimationData playerAnimData;


    private void Start()
    {
        InitDirectionBar();
        initMovementSpeed = playerMovementSpeed.Value;
    }
    private void Update()
    {
        PickUpBall();
        ThrowBall();
        UpdateDirectionBar();
    }

    private void InitDirectionBar()
    {
        directionBar.SetActive(false);
        directionFillBar.maxValue = maxForce;
        directionFillBar.value = 0;
    }


    private void PickUpBall()
    {
        if (ball != null)
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Ball>().SetSphereTrigger(true);
            ballOnHand = true;
            ball.transform.position = ballPlaceHolder.position;
        }
    }

    private void ThrowBall()
    {
        IncreaseForce();
        if (Input.GetKeyUp(playerControls.GetAttackKey) && ballOnHand)
        {
            ball.GetComponent<Ball>().SetSphereTrigger(false);
            ball.GetComponent<Rigidbody>().AddForce(transform.forward * ballForce, ForceMode.Impulse);
            ball.GetComponent<Animator>().SetTrigger("Stretch");
            StartCoroutine(ball.GetComponent<Ball>().AllowBallToBePicked());
            //ball.GetComponent<Ball>().BallTaken = false;
            ball.transform.forward = transform.forward;
            ballOnHand = false;
            ball = null;
            ballForce = 0f;
            directionFillBar.value = 0;
            directionBar.SetActive(false);
            playerMovementSpeed.Value = initMovementSpeed;
            playerAnimData.IsThrowing = false;
            playerAnimData.BallOnHand = false;
        }
    }

    private void IncreaseForce()
    {
        if (Input.GetKey(playerControls.GetAttackKey) && ballOnHand)
        {
            if (ballForce < maxForce)
            {
                playerAnimData.IsThrowing = true;
                playerMovementSpeed.Value = 0.5f;
                velocity.Value = 0f;
                directionBar.SetActive(true);
                ballForce += Time.deltaTime * forceIncreaseRate;
            }
        }
    }

    private void UpdateDirectionBar()
    {
        directionFillBar.value = ballForce;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!collision.gameObject.GetComponent<Ball>().BallTaken && !ballOnHand)
            {
                ball = collision.gameObject;
                ball.GetComponent<Ball>().BallTaken = true;
                playerAnimData.BallOnHand = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (ball != null)
        {
            ball.GetComponent<Ball>().BallTaken = true;
        }
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
}
