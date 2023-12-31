using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DodgeBall : MonoBehaviour
{

    [Header("Ball Settings")]
    [SerializeField] float ballForce = 50f;
    [SerializeField] FloatReference maxForce;
    [SerializeField] FloatReference forceIncreaseRate;
    [SerializeField] bool allowToThrow = true;
    bool ballCharging;

    [Header("DodgeBall Reference")]
    [SerializeField] GameObject ball;
    [SerializeField] bool ballOnHand;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] Transform ballPlaceHolder;
    [SerializeField] FloatReference velocity;
    [SerializeField] FloatReference playerMovementSpeed;
    CollisionDetection collisionDetection;
    [SerializeField] bool isStunned;
    float initMovementSpeed;

    [Header("Direction UI")]
    [SerializeField] GameObject directionBar;
    [SerializeField] Slider directionFillBar;

    [Header("Player Animation")]
    [SerializeField] PlayerAnimationData playerAnimData;
    [Space]
    [SerializeField] UnityEvent ChargeThrow;
    [SerializeField] UnityEvent BallThrow;

    private void Start()
    {
        InitDirectionBar();
        initMovementSpeed = playerMovementSpeed.Value;
        collisionDetection = GetComponentInParent<CollisionDetection>();
    }

    private void Update()
    {
        IfBallIsCollided();

        if (!isStunned)
        {
            PickUpBall();
            ThrowBall();
            UpdateDirectionBar();
        }
    }

    private void InitDirectionBar()
    {
        directionBar.SetActive(false);
        directionFillBar.maxValue = maxForce.Value;
        directionFillBar.value = 0;
    }

    void IfBallIsCollided()
    {
        if (collisionDetection.BallDetected != null && !ballOnHand)
        {
            ball = collisionDetection.BallDetected;
            ball.GetComponent<Ball>().BallTaken = true;
            playerAnimData.BallOnHand = true;
        }
    }

    private void PickUpBall()
    {
        if (ball != null)
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Ball>().SetSphereTrigger(true);
            ball.GetComponent<Ball>().PreviousOwner = gameObject;
            ballOnHand = true;
            ball.transform.position = ballPlaceHolder.position;
        }
    }

    private void ThrowBall()
    {
        IncreaseForce();
        if (Input.GetKeyDown(playerControls.GetDashKey)) //fake throw
        {
            ballCharging = false;
            allowToThrow = false;
            ballForce = 0f;
            directionFillBar.value = 0;
            playerMovementSpeed.Value = initMovementSpeed;
            directionBar.SetActive(false);
            playerAnimData.IsThrowing = false;
            StartCoroutine(ThrowDelay());
            return;
        }
        else if (Input.GetKeyUp(playerControls.GetAttackKey) && ballOnHand && allowToThrow)
        {
            BallThrow.Invoke();
            ballCharging = false;
            ball.GetComponent<Ball>().SetSphereTrigger(false);
            ball.GetComponent<Rigidbody>().AddForce(transform.forward * ballForce, ForceMode.Impulse);
            StartCoroutine(ball.GetComponent<Ball>().AllowBallToBePicked());
            ball.transform.forward = transform.forward;
            ballOnHand = false;
            ball = null;
            collisionDetection.BallDetected = null;
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
        if (Input.GetKey(playerControls.GetAttackKey) && ballOnHand && allowToThrow)
        {
            if (ballForce < maxForce.Value)
            {
                ChargeBallVfx();
                playerAnimData.IsThrowing = true;
                playerMovementSpeed.Value = 0.5f;
                velocity.Value = 0f;
                directionBar.SetActive(true);
                ballForce += Time.deltaTime * forceIncreaseRate.Value;
            }
        }
    }

    void ChargeBallVfx()
    {
        if (!ballCharging)
        {
            ballCharging = true;
            ChargeThrow.Invoke();
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(0f);
        allowToThrow = true;
    }

    private void UpdateDirectionBar()
    {
        directionFillBar.value = ballForce;
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
    public FloatReference PlayerVelocity { set => velocity = value; }
    public FloatReference PlayerMovementSpeed { set => playerMovementSpeed = value; }
    public PlayerAnimationData PlayerAnimData { set => playerAnimData = value; }

    public bool IsStunned { set => isStunned = value; }
}
