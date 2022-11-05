using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Entity<THealth> : IUpdateble, IEnable where THealth : Health
{
    public event Action Moved;
    public Movement Movement => _movement;
    public THealth Health => _health;

    protected THealth _health;
    private Movement _movement = new Movement();

    public Entity(THealth health)
    {
        _health = health;
    }

    public void Initialize(float baseHealth, float baseSpeed, AttributeBonuses attributeBonuses)
    {
        _movement.Initialize(baseSpeed, attributeBonuses);
        _health.Initialize(baseHealth);
    }

    public virtual void OnEnable()
    {
        _health.Died += OnDied;
        _health.ValueChanged += OnHealthChanged;
        _movement.Moved += Moved.Invoke;
    }

    public virtual void OnDisable()
    {
        _health.Died -= OnDied;
        _health.ValueChanged -= OnHealthChanged;
        _movement.Moved -= Moved.Invoke;
    }

    public virtual void Update(float deltaTime) { }

    public void ApplyDamage(int damage, DamageType type)
    {
        _health.ApplyDamage(damage, type);
    }

    public void ApplyHeal(int healPoints, bool isPure = false)
    {
        _health.ApplyHeal(healPoints, isPure);
    }
   
    private void OnDied()
    {
    }

    private void OnHealthChanged(float difference)
    {
    }

    public static implicit operator Entity<THealth>(Character<CharacterHealth> character)
    {
        return character;
    }
}
