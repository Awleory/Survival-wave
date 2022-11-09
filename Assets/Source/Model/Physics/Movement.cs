using System;
using UnityEngine;

public class Movement : IMovable, IUpdateble
{
    public event Action Moved;
    public event Action StoppedMove;

    public Vector2 Direction => _directionVelocity;

    private float _baseSpeed;
    private AttributeBonuses _attributeBonuses;
    private Vector2 _directionVelocity;

    public float Speed { get; private set; }
    public Vector2 Position { get; private set; }

    ~Movement()
    {
        _attributeBonuses.SpeedChanged -= OnSpeedChanged;
    }

    public void Initialize(float baseSpeed, AttributeBonuses attributeBonuses)
    {
        _baseSpeed = baseSpeed;
        _attributeBonuses = attributeBonuses;
        _attributeBonuses.SpeedChanged += OnSpeedChanged;
        OnSpeedChanged();
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

    private void OnSpeedChanged()
    {
        Speed = Math.Min(_baseSpeed + _attributeBonuses.Speed, Config.MaxRunSpeed);
    }
}
