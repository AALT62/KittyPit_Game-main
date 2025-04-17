using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.SceneView;

public class PlayerMove : MonoBehaviour
{
    public float jumpSpeed = 2.0f;
    public float gravity = 2.0f;
    private Vector3 playerVelocity;
    
    public float jumpPower = 1f; // 1 is ok for -9.8 gravity
    private Vector3 movingDirection = Vector3.zero;


    CharacterController controller;

    public float Speed;

    public Transform Cam;


    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity += Physics.gravity * Time.deltaTime;

        controller.Move(playerVelocity);

        if (controller.isGrounded)
        {
            playerVelocity.y = Input.GetKeyDown(KeyCode.Space) ? jumpPower : 0;
        }

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = -.00001f;



        controller.Move(Movement);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<CameraMove>().sensivity * Time.deltaTime);


            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }
    }

}