using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthPolicy
{
    public event Action<int> MaxValueChanged;

    public int MaxHealth { get; }

    public abstract int CalculateDamage(int damage);

    public abstract int CalculateHeal(int healPoints);

    public abstract void CalculateMaxHealth();
}
