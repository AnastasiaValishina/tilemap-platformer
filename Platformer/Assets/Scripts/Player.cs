using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;

    Rigidbody2D rb2d;
    Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

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
