using System;
using UnityEngine;

public class Movement : IMovable, IUpdateble
{
    public event Action Moved;
    public event Action StoppedMove;

    public Vector2 Direction => _directionVelocity;
    public float Speed => _speed;
    public Vector2 Position { get; private set; }

    private float _speed;
    private Vector2 _directionVelocity;
    private bool _canMove = true;

    public void Initialize(Vector2 startPosition, float speed = 1)
    {
        Position = startPosition;
        _speed = speed;
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
        if (_canMove)
            Position = Vector2.MoveTowards(Position, Position + _directionVelocity, _speed * deltaTime);
    }

    public void Freeze()
    {
        _canMove = false;
    }

    public void UnFreeze()
    {
        _canMove = true;
    }

    public void SetPosition(Vector2 newPosition)
    {
        Position = newPosition;
        Moved?.Invoke();
    }

    public void OnSpeedChanged(float speed)
    {
        _speed = Math.Min(Config.MaxRunSpeed, speed);
    }
}
