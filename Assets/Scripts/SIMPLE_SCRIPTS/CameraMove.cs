using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float distance = 7.0f;  // Distance from the player
    public float height = 3.0f;  // Height of the camera from the player
    public float rotationSpeed = 3.0f;  // Speed of camera rotation
    public float sensitivity = 3.0f;  // Mouse sensitivity

    private float currentX = 0.0f;  // Horizontal rotation
    private float currentY = 15.0f;  // Vertical rotation (angle)

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock cursor to the center
        Cursor.visible = false;  // Hide the cursor
    }

    private void LateUpdate()
    {
        // Rotate the camera based on mouse input
        currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;  // Horizontal rotation (camera)
        currentY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;  // Vertical rotation (camera)

        // Clamp vertical rotation to prevent flipping
        currentY = Mathf.Clamp(currentY, -45f, 45f);

        // Calculate the new position for the camera based on player position and angle
        Vector3 offset = new Vector3(0, height, -distance);

        // Rotate the camera around the player
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Position the camera behind the player
        transform.position = player.position + rotation * offset;

        // Camera always looks at the player's back (look at the player's back, not their head)
        transform.LookAt(player.position + Vector3.up * height);
    }
}
