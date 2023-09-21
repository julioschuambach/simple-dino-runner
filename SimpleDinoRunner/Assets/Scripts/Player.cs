using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;
    private AudioSource audioSource;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 jumpForce;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dieClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && isGrounded)
            Jump();
    }

    private void LateUpdate()
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        audioSource.PlayOneShot(jumpClip);
        SetGrounded(false);
        rigidbody.AddForce(jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(dieClip);
            animator.SetBool("isDead", true);
            GameManager.instance.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGrounded(true);
        }
    }

    private void SetGrounded(bool value)
        => isGrounded = value;
}
