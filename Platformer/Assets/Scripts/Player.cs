using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    Rigidbody2D rb2d;
    Animator animator;
    Collider2D collider;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(horizontalInput * runSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        if (rb2d.velocity.x != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void Jump()
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                rb2d.velocity += jumpVelocity;
            }
        }
    }

    private void FlipSprite()
    {
        if (rb2d.velocity.x < 0f)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (rb2d.velocity.x > 0f)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }
}
