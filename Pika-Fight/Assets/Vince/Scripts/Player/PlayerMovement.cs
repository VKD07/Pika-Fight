using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationSpeed = 720f;
    Vector3 playerPos;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        float horizontal = Input.GetAxis($"{playerControls.GetMovementAxes}_Horizontal");
        float vertical = Input.GetAxis($"{playerControls.GetMovementAxes}_Vertical");

        playerPos = new Vector3(vertical,0,horizontal);
        rb.velocity = playerPos * movementSpeed;
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
}
