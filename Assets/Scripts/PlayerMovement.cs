using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        

        // Get input from the horizontal axis (left arrow, right arrow, A, D)
        float moveInput = Input.GetAxis("Horizontal");

        if(moveInput < 0 || moveInput > 0)
        {
            animator.SetFloat("Walk", 1);
        }else
        {
            animator.SetFloat("Walk", 0);
        }

        if (moveInput < 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput > 0)
        {
            sprite.flipX = false;
        }

        // Set the movement vector based on input
        movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void FixedUpdate()
    {
        // Apply the movement to the player's Rigidbody2D
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }
}
