using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float initialGravity;
    bool isAlive = true;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        initialGravity = rb2d.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
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
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                rb2d.velocity += jumpVelocity;
            }
        }
    }

    private void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            animator.SetBool("Climbing", false);
            rb2d.gravityScale = initialGravity;
            return; 
        }

        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0)
        {
            Vector2 climpVelocity = new Vector2(rb2d.velocity.x, verticalInput * climbSpeed);
            rb2d.velocity = climpVelocity;
            rb2d.gravityScale = 0;
            animator.SetBool("Climbing", true);
        }
        else 
        {
            animator.SetBool("Climbing", false);
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

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            animator.SetTrigger("Die");
            rb2d.velocity = deathKick;
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
