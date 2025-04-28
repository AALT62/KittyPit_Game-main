using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;  // Movement speed
    public float sprintSpeed = 15.0f;  // Sprint speed
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 velocity;

    private Vector3 spawnPoint = new Vector3(0.75f, 100.2f, -77.44f);  // Spawn point

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input for horizontal and vertical movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Set movement direction based on input
        movement = new Vector3(horizontal, 0, vertical).normalized;

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = movement * sprintSpeed;
        }
        else
        {
            velocity = movement * moveSpeed;
        }

        // Apply movement
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    public void PlayerRespawn()
    {
        rb.linearVelocity = Vector3.zero; // Reset the velocity
        transform.position = spawnPoint; // Reset the position to spawn point
    }
}
