using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : IUpdateble, IEnable
{
    public MovementPhysics MovementPhysics { get; private set; }
  
    private Health _health;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    public void Initialize(BoxCollider2D boxCollider2D, Rigidbody2D rigidbody2D, HealthPolicy healthPolicy)
    {
        _boxCollider2D = boxCollider2D;
        _rigidbody2D = rigidbody2D;
        MovementPhysics = new MovementPhysics(_rigidbody2D);
        _health = new Health(healthPolicy);
    }

    public void OnEnable()
    {
        _health.OnEnable();

        _health.Died += OnDied;
        _health.ValueChanged += OnHealthChanged;
    }

    public void OnDisable()
    {
        _health.Died -= OnDied;
        _health.ValueChanged -= OnHealthChanged;

        _health.OnDisable();
    }

    public virtual void Update(float deltaTime) 
    {
        MovementPhysics.Update(deltaTime);
    }

    public void FixedUpdate(float deltaTime) { }

    public void ApplyDamage(int damage, bool isPure = false)
    {
        _health.ApplyDamage(damage, isPure);
    }

    public void Heal(int healPoints, bool isPure = false)
    {
        _health.ApplyHeal(healPoints, isPure);
    }
   
    private void OnDied()
    {

    }

    private void OnHealthChanged(int difference)
    {

    }
}
