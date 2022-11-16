using System;
using UnityEngine;

public class Movement : IMovable, IUpdateble
{
    public event Action Moved;
    public event Action StoppedMove;

    public Vector2 Direction => _directionVelocity;
    public float Speed => Math.Min(_baseSpeed * _speedRate, Config.MaxRunSpeed);
    public Vector2 Position { get; private set; }

    private float _baseSpeed;
    private float _speedRate = 1f;
    private Vector2 _directionVelocity;

    public void Initialize(float baseSpeed, Vector2 startPosition)
    {
        _baseSpeed = baseSpeed;
        Position = startPosition;
    }

    public void Move(Vector2 direction)
    {
        if (_directionVelocity != direction || direction != Vector2.zero)
        {
            _directionVelocity = direction;
            Moved?.Invoke();
        }

        if (direction == Vector2.zero)
        {
            StoppedMove?.Invoke();
        }
    }

    public void Update(float deltaTime)
    {
        Position = Vector2.MoveTowards(Position, Position + _directionVelocity, Speed * deltaTime);
    }

    public void OnBonusSpeedChanged(float speedRate)
    {
        _speedRate = speedRate;
    }
}
