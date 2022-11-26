using System;
using UnityEngine;

public class Bullet : IUpdateble, IEnable
{
    public event Action Moved;
    public event Action Destroyed;

    public float Damage { get; private set; }
    public Movement Movement { get; private set; }

    private bool _isPureDamage; 
    private float _maxTargetHits;
    private float _targetHits = 0;
    private Vector2 _direction;

    public Bullet(BulletConfig bulletConfig, Vector2 direction, Vector2 startPosition)
    {
        Damage = bulletConfig.Damage;
        _direction = direction;
        _maxTargetHits = bulletConfig.MaxTargetHits;
        _isPureDamage = bulletConfig.IsPureDamage;

        Movement = new Movement();
        Movement.Initialize(startPosition, bulletConfig.Speed);
        Movement.Move(_direction);
    }

    public void OnEnable()
    {
        Movement.Moved += OnMoved;
    }

    public void OnDisable()
    {
        Movement.Moved -= OnMoved;
    }

    public void Update(float deltaTime)
    {
        Movement.Move(_direction);
        Movement.Update(deltaTime);
    }

    public void ProcessCollision(Character character)
    {
        if (character.IsAlive == false)
            return;

        if (_targetHits >= _maxTargetHits)
        {
            Destroy();
            return;
        }

        character.ApplyDamage(Damage, _isPureDamage);
        _targetHits++;
    }

    public void Reset(Vector2 position, Vector2 direction)
    {
        _targetHits = 0;
        Movement.SetPosition(position);
        _direction = direction;
    }

    private void OnMoved()
    {
        Moved?.Invoke();
    }

    private void Destroy()
    {
        Destroyed?.Invoke();
    }
}
