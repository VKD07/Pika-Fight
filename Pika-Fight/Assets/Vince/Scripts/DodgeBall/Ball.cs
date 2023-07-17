using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
    [SerializeField] bool ballTaken;
    Rigidbody rb;
    SphereCollider sphereCollider;
    TrailRenderer trailRenderer;
    [SerializeField] float velocity;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        ActivateTrail();
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

    public bool BallTaken
    {
        get { return ballTaken; }
        set { ballTaken = value; }
    }
}
