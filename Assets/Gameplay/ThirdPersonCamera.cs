using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] public Transform target;         // Reference to the player's transform

    public Vector3 offset = new Vector3(0f, 2f, -5f);    // Offset from the player's position
    public float rotationSpeed = 1f; // Speed of rotation around the player
    private float rotationAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position based on the player's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed);

        // Perform camera rotation based on input
        HandleRotationInput();
    }
    private void HandleRotationInput()
    {
        // Get the horizontal input for rotation
        float rotationInput = Input.GetAxis("Horizontal");

        // Calculate the new rotation angle
        rotationAngle += rotationInput * rotationSpeed;

        // Set the camera's rotation around the player
        Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);
        transform.position = target.position + rotation * offset;

        // Make the camera look at the player's position
        transform.LookAt(target.position);
    }
}
