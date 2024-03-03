using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : State
{
    public float jumpForce;
    public AnimationClip animationClip;
    public override void EnterState()
    {
        animator.Play(animationClip.name);
    }
    public override void UpdateState()
    {
        float time = Helpers.Map(rigidBody.velocity.y, jumpForce, -jumpForce, 0, 1, true);
        animator.Play(animationClip.name, 0, time);
        animator.speed = 0;

        if (controller.grounded)
        {
            isComplete = true;
        }
    }
    public override void ExitState()
    {

    }
}
