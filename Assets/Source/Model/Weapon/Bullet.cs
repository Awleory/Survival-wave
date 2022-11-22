using System;
using UnityEngine;

public class Bullet : IUpdateble
{
    public event Action Moved;
    public event Action Destroyed;

    public float Damage { get; private set; }
    public Movement Movement { get; private set; }

    private bool _isPureDamage; 
    private float _maxTargetHits;
    private float _targetHits = 0;
    private Vector2 _direction;
    private float _passedTime = 0;

    private const float _liveTime = 2;

    public Bullet(BulletConfig bulletConfig, Vector2 direction, Vector2 startPosition)
    {
        Damage = bulletConfig.Damage;
        _direction = direction;
        _maxTargetHits = bulletConfig.MaxTargetHits;
        _isPureDamage = bulletConfig.IsPureDamage;

        Movement = new Movement();
        Movement.Initialize(startPosition, bulletConfig.Speed);
        Movement.Move(_direction);

        Movement.Moved += OnMoved;
    }

    public void Update(float deltaTime)
    {
        Movement.Move(_direction);
        Movement.Update(deltaTime);

        _passedTime += deltaTime;
        if (_passedTime >= _liveTime)
            Destroy();
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

    private void OnMoved()
    {
        Moved?.Invoke();
    }

    private void Destroy()
    {
        Destroyed?.Invoke();
    }
}
