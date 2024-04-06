using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredGhostMover : CharacterMover
{
    public override event Action<Vector2> OnMovementDirectionComputed;

    

    protected override void Move(Vector2 inputDirection)
    {
        
    }
}
