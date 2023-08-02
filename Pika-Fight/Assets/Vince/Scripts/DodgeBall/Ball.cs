using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
    [SerializeField] bool ballTaken;
    [SerializeField] float ballDamage;
    [Header("Raycast")]
    [SerializeField] float rayDistance = 1f;
    [SerializeField] LayerMask playerLayer;
    RaycastHit hit;
    [Header("Events")]
    [SerializeField] UnityEvent OnImpact;
    [SerializeField] UnityEvent OnPlayerImpact;
    Rigidbody rb;
    SphereCollider sphereCollider;
    TrailRenderer trailRenderer;
    //To Collect the damage dealt from previous owner
    [SerializeField] GameObject previousOwner;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        ActivateTrail();
        SetBallDamage();
        DetectPlayer();
    }

    private void ActivateTrail()
    {
        if (rb.velocity.magnitude > 20)
        {
            trailRenderer.enabled = true;
        }
        else if (rb.velocity.magnitude < 5)
        {
            trailRenderer.enabled = false;
        }
    }

    private void SetBallDamage()
    {
        ballDamage = rb.velocity.magnitude;
        //if (!rb.isKinematic)
        //{
        //    //ballTaken = false;
        //}
    }

    private void DetectPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, playerLayer))
        {
            if(rb.velocity.magnitude > 10 && ballTaken)
            {
                hit.collider.GetComponent<ReceiveDamage>().GetDamage(ballDamage);
                ChickenMode(hit.collider.gameObject, ballDamage);
                OnPlayerImpact.Invoke();
            }
        }
    }

    public void SetSphereTrigger(bool enable)
    {
        if (!enable)
        {
            StartCoroutine(SetSphereTriggerToFalse(false));
        }
        else
        {
            sphereCollider.isTrigger = enable;
        }
    }

    IEnumerator SetSphereTriggerToFalse(bool enable)
    {
        yield return new WaitForSeconds(0.1f);
        sphereCollider.isTrigger = enable;
    }

    public IEnumerator AllowBallToBePicked()
    {
        yield return new WaitForSeconds(2);
        ballTaken = false;
    }

    //for the chicken game mode
    void ChickenMode(GameObject player, float damageDealt)
    {
        if(player.GetComponentInChildren<ChickenMode>().enabled && previousOwner != null)
        {
            previousOwner.GetComponentInParent<PlayerConfigBridge>().PlayerConfig.DamageDealtToChicken += damageDealt;
        }
    }

    public bool BallTaken
    {
        get { return ballTaken; }
        set { ballTaken = value; }
    }

    public float GetBallDamage => ballDamage;

    public GameObject PreviousOwner { get => previousOwner; set { previousOwner = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > 10)
        {
            OnImpact.Invoke();
        }

        if (collision.gameObject.tag == "Player" && rb.velocity.magnitude > 20 && ballTaken)
        {
            OnPlayerImpact.Invoke();
            collision.gameObject.GetComponent<ReceiveDamage>().GetDamage(ballDamage);
            ChickenMode(collision.gameObject, ballDamage);
        }
    }
}
