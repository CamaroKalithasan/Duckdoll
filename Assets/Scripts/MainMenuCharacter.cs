using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacter : MonoBehaviour
{
    Rigidbody rb;
    private int jumpNum = 0;
    private bool isJumping;
    public AudioSource audioSource;
    public Animator animator;
    public string idleAnimationName = "Idle_A";
    public string jumpAnimationName = "Fly";
    public string spinAnimationName = "Spin";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (!isJumping)
        {
            animator.Play(idleAnimationName);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && !isJumping)
        {
            rb.linearVelocity = new Vector3(0, 5f, 0);
            isJumping = true;
            animator.Play(jumpAnimationName);
            audioSource.Play();
            jumpNum++;
            if (jumpNum == 3)
            {
                animator.Play(spinAnimationName);
                jumpNum = 0;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
