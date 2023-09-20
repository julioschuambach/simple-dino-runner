using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 jumpForce;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && isGrounded)
        {
            Debug.Log("Jump()");
            Jump();
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        SetGrounded(false);
        rigidbody.AddForce(jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            Debug.LogError("GAME OVER!");
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
