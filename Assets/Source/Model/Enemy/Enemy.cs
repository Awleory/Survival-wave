using System;
using UnityEngine;

public class Enemy : Character
{
    public Player Target { get; private set; }
    public float DistanceToTarget => Vector2.Distance(Target.Movement.Position, Movement.Position); 

    public Enemy(Player target)
    {
        Target = target;
    }
}
