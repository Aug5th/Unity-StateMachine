using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public BoxCollider2D groundCheck;
    public Animator animator;
    public LayerMask groundMask;

    public float maxSpeed;
    public float jumpForce;
    [Range(0f, 1f)]
    public float acceleration;

    public bool grounded;

    float xInput;
    float yInput;



    private void Start()
    {
       
    }

    private void Update()
    {
        GetInputs();
        Jump();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }


    private void GetInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    public void Move()
    {
        if(Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rigidBody.velocity.x + increment, -maxSpeed, maxSpeed);
            rigidBody.velocity = new Vector2(newSpeed, rigidBody.velocity.y);
            FaceInput();
        }

        
    }

    private void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }
}
