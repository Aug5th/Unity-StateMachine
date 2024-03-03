using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public AnimationClip animationClip;
    public override void EnterState()
    {
        animator.Play(animationClip.name);
    }
    public override void UpdateState()
    {
        if(!controller.grounded || controller.xInput != 0)
        {
            isComplete = true;
        }
    }
    public override void ExitState()
    {

    }
}
