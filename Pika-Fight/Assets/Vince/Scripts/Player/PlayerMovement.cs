using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField] float rotationSpeed = 720f;
    [SerializeField] FloatReference velocity;
    [SerializeField] FloatReference movementSpeed;
    Vector3 playerPos;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed.Value = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.y -= 2f;
        rb.velocity = velocity;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis($"{playerControls.GetMovementAxes}_Horizontal");
        float vertical = Input.GetAxis($"{playerControls.GetMovementAxes}_Vertical");

        playerPos = new Vector3(vertical,0,-horizontal);
        velocity.Value = playerPos.magnitude;
        rb.velocity = playerPos * movementSpeed.Value;
    }

    private void Rotate()
    {
        if (playerPos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(playerPos, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public PlayerControls SetPlayerControls { set { playerControls = value; } }
    public FloatReference PlayerVelocity { set => velocity = value; }
    public FloatReference PlayerMovementSpeed { set => movementSpeed = value; }
}
