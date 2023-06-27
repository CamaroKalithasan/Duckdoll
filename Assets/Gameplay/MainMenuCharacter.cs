using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacter : MonoBehaviour
{
    Rigidbody rb;
    private bool isJumping;
    public AudioSource audioSource;
    public Animator animator;
    public string idleAnimationName = "Idle_A";
    public string jumpAnimationName = "Fly";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!isJumping)
        {
            animator.Play(idleAnimationName);
        }

        if ((Input.GetKey("space") || Input.GetKey("joystick button 0")) && !isJumping)
        {
            rb.velocity = new Vector3(0, 5f, 0);
            isJumping = true;
            animator.Play(jumpAnimationName);
            audioSource.Play();
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
