using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public AnimationClip animationClip;
    public override void EnterState()
    {
        animator.Play(animationClip.name);
    }
    public override void UpdateState()
    {
        float veilX = rigidBody.velocity.x;
        animator.speed = Helpers.Map(controller.maxSpeed, 0, 1, 0, 1.6f, true);
        
        if(!controller.grounded || Mathf.Abs(veilX) < 0.1f)
        {
            isComplete = true;
        }
    }
    public override void ExitState()
    {

    }
}
