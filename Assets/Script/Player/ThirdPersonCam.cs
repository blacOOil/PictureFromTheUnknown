using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform target; // The player character
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset
    public float rotationSpeed = 5.0f; // Speed of rotation

    private float yaw = 0f; // Horizontal rotation (only Y-axis movement)

    void LateUpdate()
    {
        if (target == null)
            return;

        // Get input for horizontal camera rotation
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;

        // Rotate the camera only around the Y-axis (horizontal)
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        transform.position = target.position + rotation * offset;

        // Make the camera look at the target
        transform.LookAt(target.position + Vector3.up * 2); // Slightly above the target
    }
}
