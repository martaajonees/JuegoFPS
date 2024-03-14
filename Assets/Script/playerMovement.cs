using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Transform camara;
    float vMouse;
    float hMouse;
    float yReal = 0.0f;
    public float horizontalSpeed;
    public float verticalSpeed;

    public CharacterController controller;
    public float speed = 12f;
    float x;
    float z;
    Vector3 move;

    Vector3 velocity;
    public float gravity = -15f;
    bool isGrounded = false;

    // jump
    public float jumpForce = 1f;
    float jumpValue;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        jumpValue = Mathf.Sqrt(jumpForce * -2f * gravity);
    }
    void Update()
    {
        lookMouse();

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }

        Movement();
        Jump();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void lookMouse()
    {
        hMouse = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
        vMouse = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

        yReal -= vMouse;
        yReal = Mathf.Clamp(yReal, -90f, 90f);
        transform.Rotate(0f, hMouse, 0f);
        camara.localRotation = Quaternion.Euler(yReal, 0f, 0f);
    }

    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            velocity.y = jumpValue;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if(hit.collider.CompareTag("Floor"))
        {
            if(!isGrounded)
            {
                isGrounded = true;
            }
        }
        
    }
}
