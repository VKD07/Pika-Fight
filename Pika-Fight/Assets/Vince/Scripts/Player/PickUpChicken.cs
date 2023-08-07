using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChicken : MonoBehaviour
{
    [SerializeField] GameObject chicken;
    [SerializeField] FloatReference playerVelocity;
    [SerializeField] Transform chickenPos;
    [SerializeField] float percentageRate = 0.5f;
    [SerializeField] FloatReference playerHealth;
    PlayerConfigBridge configBridge;
    CollisionDetection collisionDetection;
    private void Start()
    {
        configBridge = GetComponentInParent<PlayerConfigBridge>();
        collisionDetection = GetComponentInParent<CollisionDetection>();
    }

    void Update()
    {
        IfChickenIsCollided();
        PickUpAndHold();
        DropChicken();
    }

    private void IfChickenIsCollided()
    {
        if (collisionDetection.ChickenDetected != null)
        {
            if (!collisionDetection.ChickenDetected.GetComponent<ChickenStateManager>().ChickenIsTaken)
            {
                chicken = collisionDetection.ChickenDetected;
            }
        }
    }

    private void PickUpAndHold()
    {
        if (chicken != null && playerHealth.Value > 0)
        {
            IncreaseHoldPercentage();
            chicken.transform.position = chickenPos.position;
            chicken.transform.forward = chickenPos.forward;
            chicken.GetComponent<ChickenStateManager>().enabled = false;
            chicken.GetComponent<ChickenStateManager>().ChickenIsTaken = true;
            chicken.GetComponent<Animator>().SetBool("Walk", false);
            if (playerVelocity.Value > 0)
            {
                chicken.GetComponent<Animator>().SetBool("Flap", true);
                chicken.GetComponent<Animator>().SetBool("Eat", false);
            }
            else
            {
                chicken.GetComponent<Animator>().SetBool("Flap", false);
            }
        }
    }

    void IncreaseHoldPercentage()
    {
        if(configBridge.PlayerConfig.HoldPercentage < 100)
        {
            configBridge.PlayerConfig.HoldPercentage += percentageRate * Time.deltaTime;
        }
    }

    public void DropChicken()
    {
        if (chicken != null && playerHealth.Value <=0)
        {
            chicken.GetComponent<ChickenStateManager>().ChickenIsTaken = false;
            chicken.GetComponent<ChickenStateManager>().enabled = true;
            chicken.GetComponent<Animator>().SetBool("Flap", false);
            chicken = null;
            collisionDetection.ChickenDetected = null;
        }
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Chicken")
    //    {
    //        if (!collision.GetComponent<Movement_Chicken>().ChickenIsTaken)
    //        {
    //            chicken = collision.gameObject;
    //        }
    //    }
    //}
    public FloatReference PlayerVelocity { set => playerVelocity = value; }
    public FloatReference PlayerHealth { set => playerHealth = value; }
}
