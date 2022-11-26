using System;
using UnityEngine;

public class Enemy : Character
{
    public event Action<Enemy> GotRespawnDistance;

    public Player Target { get; private set; }
    public float DistanceToTarget => Vector2.Distance(Target.Movement.Position, Movement.Position);

    private const float _minRespawnDistanceToTarget = 12f;

    public Enemy(Player target)
    {
        Target = target;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (DistanceToTarget >= _minRespawnDistanceToTarget)
            GotRespawnDistance?.Invoke(this);
    }
}
