using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public IdleState idleState;
    public AirState airState;
    public RunState runState;
    public KneelState kneelState;

    State state;

    public Rigidbody2D rigidBody;
    public BoxCollider2D groundCheck;
    public Animator animator;
    public LayerMask groundMask;


    [Range(0f, 1f)]
    public float acceleration;
    public float maxSpeed;

    public bool grounded { get; private set; }
    public float xInput { get; private set; }
    public float yInput { get; private set; }

    private void Start()
    {
        idleState.Setup(rigidBody, animator,this);
        runState.Setup(rigidBody, animator, this);
        airState.Setup(rigidBody, animator, this);
        kneelState.Setup(rigidBody, animator, this);
        state = idleState;
    }

    private void Update()
    {
        GetInputs();
        Jump();

        if(state.isComplete)
        {
            SelectState();
        }
        state.UpdateState();
    }

    private void SelectState()
    {
        if(grounded)
        {
            if(xInput == 0 && yInput == 0)
            {
                state = idleState;
            }
            else if(xInput == 0 && yInput < 0)
            {
                state = kneelState;
            }
            else
            {
                state = runState;
            }
        }
        else
        {
            state = airState;
        }
        state.EnterState();
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
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, airState.jumpForce);
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
