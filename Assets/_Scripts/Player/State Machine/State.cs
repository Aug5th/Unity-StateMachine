using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;

    public float time => Time.time - startTime;

    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected PlayerController controller;

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void FixedUpdateState() { }
    public virtual void ExitState() { }

    public void Setup(Rigidbody2D rigidBody, Animator animator, PlayerController controller)
    {
        this.rigidBody = rigidBody;
        this.animator = animator;
        this.controller = controller;
    }

}
