using UnityEngine;

// Automatically add Rigidbody to the game object
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    private Vector3 spawnPoint = new Vector3(0.75f, 100.2f, -77.44f);
    public float _speed = 6;
    public float _jumpForce = 6;
    private Rigidbody _rig;
    private Vector2 _input;
    private Vector3 _movementVector;
    public Transform cam; // Reference to the camera (drag the camera here in the inspector)

    private void Start()
    {
        _rig = GetComponent<Rigidbody>();
        // Prevent the player from rotating when physics is applied
        _rig.freezeRotation = true;
    }

    public void PlayerRespawn()
    {
        transform.position = spawnPoint; // Reset the position to the spawn point
    }

    private void Update()
    {
        // Cleaner way to get input
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Check for jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Keep the movement vector aligned with the camera rotation
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f; // Make sure movement stays on the ground (no vertical movement)
        right.y = 0f;

        _movementVector = _input.x * right * _speed + _input.y * forward * _speed;

        // Apply the movement vector to the Rigidbody (keep Y velocity unchanged)
        _rig.linearVelocity = new Vector3(_movementVector.x, _rig.linearVelocity.y, _movementVector.z);

        // Make the player look in the direction of movement (keep the back facing the camera)
        if (_rig.linearVelocity.magnitude > 0.1f)
        {
            Vector3 direction = new Vector3(_rig.linearVelocity.x, 0f, _rig.linearVelocity.z);
            if (direction.magnitude > 0)
            {
                // Rotate the player to face the direction of movement
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
            }
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded by casting a ray downwards
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // Slightly increased raycast distance
    }

    private void Jump()
    {
        // Apply the jump force upwards
        _rig.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpForce = 10f;  // Jump force (adjusted for balance)
    public float gravity = -30f;   // Custom gravity
    private Vector3 playerVelocity;
    private Vector3 spawnPoint = new Vector3(0.75f, 100.2f, -77.44f);
    public float speed = 5f; // Movement speed
    public Transform cam;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        rb.freezeRotation = true; // Prevent rotation from physics simulation
    }

    public void PlayerRespawn()
    {
        rb.linearVelocity = Vector3.zero; // Reset the velocity
        transform.position = spawnPoint; // Reset the position to spawn point
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerRespawn();
        }

        // Check if the player is grounded
        bool isGrounded = IsGrounded();

        // Apply gravity manually if you want custom gravity control
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration); // Apply gravity manually
        }
        else
        {
            // Only allow jumping if grounded
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Set the y velocity to jump force
            }
        }

        // Handle movement
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 movement = cam.transform.right * horizontal + cam.transform.forward * vertical;
        movement.y = 0f; // Keep the y value zero for ground movement

        rb.MovePosition(transform.position + movement); // Apply movement

        // Rotate the player model based on camera's direction (not the Rigidbody rotation)
        if (movement.magnitude != 0f)
        {
            // Rotate the player model to face the movement direction, aligned with the camera
            Vector3 direction = movement.normalized;
            direction.y = 0f; // Keep rotation flat on the ground
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smoothly rotate
        }
    }

    // Check if the player is grounded
    private bool IsGrounded()
    {
        // Cast a ray downward to check if the player is on the ground
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}
*/