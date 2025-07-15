using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{

    [Header("Movement Controller")]
    public float Speed;
    public float JumpForce;
    public float GroundCheckDistance = 0.1f;
    public LayerMask GroundLayer;
  
    private Camera mainCamera;
    private Rigidbody rb;
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        mainCamera = Camera.main;
      
      
    }

    void Update()
    {

      
        MovementInput();
        JumpInput();
        CheckGroundStatus();

    }
    void MovementInput()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction in world space
        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        if (movementDirection.magnitude >= 0.1f)
        {
            // Apply movement using Rigidbody
            Vector3 newVelocity = new Vector3(movementDirection.x * Speed, rb.linearVelocity.y, movementDirection.z * Speed);
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, newVelocity, Time.deltaTime * 10f); // Smooth interpolation

            // Calculate the rotation to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation

            // Handle jump input
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }

         
        }
       
    }

    void JumpInput()
    {
        // Handle jump input
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void CheckGroundStatus()
    {
        // Perform a raycast downwards to check if the bot is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance, GroundLayer);
    }

   
}
