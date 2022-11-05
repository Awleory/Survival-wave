using System;
using UnityEngine;

public class Movement : IMovable, IUpdateble
{
    public event Action Moved;
    
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
    }

    public void Move(Vector2 direction)
    {
        _directionVelocity = direction;
        Moved?.Invoke();
    }

    public void Update(float deltaTime)
    {
        Position = Vector2.MoveTowards(Position, _directionVelocity, Speed * deltaTime);
    }

    private void OnSpeedChanged()
    {
        Speed = Math.Max(_baseSpeed + _attributeBonuses.Speed, Config.MaxRunSpeed);
    }
}
