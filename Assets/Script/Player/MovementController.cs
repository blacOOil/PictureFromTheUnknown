using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Movement settings
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 5f;
    public float gravity = -9.8f;

    // Mouse look settings
    public float mouseSensitivity = 0.2f;
    public float lookXLimit = 45f;
    public Transform _3D_cursor;

    // Components
    private CharacterController characterController;
    public Transform cameraTransform;
    private Camera mainCamera,HandheldCam;

    // Movement variables

    public Transform HandheldCamera_TransForm;

    public bool IsInFPS = false;

    public float mouseInputDelay = 1000f; // Delay in seconds

    public bool IsMoveable;


    public Vector3 movementDirection()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the movement direction relative to the camera
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Ignore the vertical direction of the camera
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Determine the movement direction
        return (cameraForward * vertical + cameraRight * horizontal);
    }


    void Start()
    {
        // Initialize components
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        

        
    }

    void Update()
    {
        if (IsMoveable)
        {
            if (IsInFPS)
            {
                HandleMouseLook();
            }
            else
            {
                Vector3 moveDirection = movementDirection();
                HandleMovement(moveDirection);
            }
        }
       
    }

    void HandleMouseLook()
    {  
        float mouseX = ((Input.mousePosition.x/Screen.width)-0.5f)*2;
        float mouseY = ((Input.mousePosition.y/Screen.height) - 0.5f)*2;

        Vector3 targetPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, mainCamera.nearClipPlane));
        
        Quaternion newRotation = Quaternion.Euler((90/2)*(-mouseY),180*mouseX,0);
        HandheldCamera_TransForm.localRotation = newRotation;
    }
    void HandleMovement(Vector3 moveDirection)
    {

      
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Convert input into world direction relative to camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ensure forward and right vectors are flat on the XZ plane
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Compute movement direction
        moveDirection = forward * vertical + right * horizontal;

        if (moveDirection.magnitude > 0.1f)
        {
            // Smoothly rotate the character to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply movement
        characterController.Move(moveDirection * walkSpeed * Time.deltaTime);

        // Apply gravity
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }
}
