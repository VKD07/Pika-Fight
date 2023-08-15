using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Dash : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] FloatReference dashForce;
    [SerializeField] FloatReference dashTime;
    [SerializeField] FloatReference dashCooldown;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] UnityEvent IsDashing;
    [SerializeField] UnityEvent IsNotDashing;
    bool isDashing;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        BeginDash();
    }

    private void BeginDash()
    {
        if (Input.GetKeyDown(playerControls.GetDashKey) && !isDashing)
        {
            IsDashing.Invoke();

            rb.velocity = transform.forward * dashForce.Value;
            isDashing = true;
            StartCoroutine(StopDash());
            StartCoroutine(EnableDash());
        }
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime.Value);
        IsNotDashing.Invoke();
        rb.velocity = Vector3.zero;
    }

    IEnumerator EnableDash()
    {
        yield return new WaitForSeconds(dashCooldown.Value);
        isDashing = false;
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
}
