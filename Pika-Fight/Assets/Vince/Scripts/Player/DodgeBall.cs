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
    [SerializeField] float maxForce = 100f;
    [SerializeField] float forceIncreaseRate = 20f;
    bool allowToThrow = true;

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
        directionFillBar.maxValue = maxForce;
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
            ballOnHand = true;
            ball.transform.position = ballPlaceHolder.position;
        }
    }

    private void ThrowBall()
    {
        IncreaseForce();
        if (Input.GetKeyDown(playerControls.GetDashKey)) //fake throw
        {
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
            ball.GetComponent<Ball>().SetSphereTrigger(false);
            ball.GetComponent<Rigidbody>().AddForce(transform.forward * ballForce, ForceMode.Impulse);
            StartCoroutine(ball.GetComponent<Ball>().AllowBallToBePicked());
            //ball.GetComponent<Ball>().BallTaken = false;
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

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(1);
        allowToThrow = true;
    }

    private void UpdateDirectionBar()
    {
        directionFillBar.value = ballForce;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.parent == transform.parent && parentRigidBody != null)
    //    {
    //        if (collision.gameObject.tag == "Ball")
    //        {
    //            if (!collision.gameObject.GetComponent<Ball>().BallTaken && !ballOnHand)
    //            {
    //                ball = collision.gameObject;
    //                ball.GetComponent<Ball>().BallTaken = true;
    //                playerAnimData.BallOnHand = true;
    //            }
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (ball != null)
    //    {
    //        ball.GetComponent<Ball>().BallTaken = true;
    //    }
    //}

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
    public FloatReference PlayerVelocity { set => velocity = value; }
    public FloatReference PlayerMovementSpeed { set => playerMovementSpeed = value; }
    public PlayerAnimationData PlayerAnimData { set => playerAnimData = value; }

    public bool IsStunned { set => isStunned = value; }
}
