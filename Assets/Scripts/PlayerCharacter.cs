using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody rb;
    private Transform mainCameraTransform;
    public AudioSource audioSource;
    public Animator animator;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;


    public string idleAnimationName = "Idle_A";
    public string jumpAnimationName = "Fly";
    public string walkAnimationName = "Walk";
    public string sitAnimationName = "Sit";
    public string spinAnimationName = "Spin";
    public string attackAnimationName = "Attack";
    //public string hitAnimationName = "Hit";

    private float noInputTimer;
    private bool isSitting;    
    private bool isJumping;
    private bool isAttacking = false;
    private int jumpNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 cameraForward = mainCameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 movement = cameraForward * verticalInput * movementSpeed;
        if (movement.magnitude > 0f && isSitting)
        {
            isSitting = false;
            animator.Play(idleAnimationName);
            ResetNoInputTimer();
        }
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        if (!isJumping && !isSitting)
        {
            if (movement.magnitude > 0f)
            {
                // The character is walking
                animator.Play(walkAnimationName);
                ResetNoInputTimer();
            }
            else
            {
                // The character is idle
                animator.Play(idleAnimationName);
            }
        }
        if (movement != Vector3.zero)
        {
            // Rotate the character to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && !isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isJumping = true;
            animator.Play(jumpAnimationName);
            audioSource.Play();
            ResetNoInputTimer();
            jumpNum++;
            if (jumpNum == 3)
            {
                animator.Play(spinAnimationName);
                jumpNum = 0;
            }
        }

        // Check for no input and isn't already sitting
        if (movement.magnitude == 0f && !isSitting)
        {
            noInputTimer += Time.deltaTime;
            if (noInputTimer >= 60f) //in seconds
            {
                animator.Play(sitAnimationName);
                isSitting = true;
            }
        }
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKey(KeyCode.JoystickButton1)) && !isAttacking && !isJumping && !isSitting)
        {
            animator.Play(attackAnimationName);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isAttacking && !isJumping && !isSitting)
        {
            movementSpeed = 5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAttacking && !isJumping && !isSitting)
        {
            movementSpeed = movementSpeed * 3;
        }
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void ResetNoInputTimer()
    {
        noInputTimer = 0f;
    }

}
