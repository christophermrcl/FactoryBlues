using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement
    private float curSpeed = 0;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameState gameState;
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
        curSpeed = moveSpeed;
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (gameState.isDialogueActive)
        {
            curSpeed = 0f;
        }
        else
        {
            curSpeed = moveSpeed;
        }

        // Get input from the horizontal axis (left arrow, right arrow, A, D)
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");

        if((moveInputHorizontal < 0 || moveInputHorizontal > 0 || moveInputVertical < 0 || moveInputVertical > 0) && !gameState.isDialogueActive)
        {
            animator.SetFloat("Walk", 1);
        }else
        {
            animator.SetFloat("Walk", 0);
        }

        if (moveInputHorizontal < 0 && !gameState.isDialogueActive)
        {
            sprite.flipX = true;
        }
        else if (moveInputHorizontal > 0 && !gameState.isDialogueActive)
        {
            sprite.flipX = false;
        }

        // Set the movement vector based on input
        movement = new Vector2(moveInputHorizontal * curSpeed, moveInputVertical * curSpeed);
    }

    void FixedUpdate()
    {
        // Apply the movement to the player's Rigidbody2D
        rb.velocity = new Vector2(movement.x, movement.y);
    }
}
