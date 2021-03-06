using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        if (IsFacingRight())
        {
            rb2d.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-Math.Sign(rb2d.velocity.x), 1f);
    }
}
