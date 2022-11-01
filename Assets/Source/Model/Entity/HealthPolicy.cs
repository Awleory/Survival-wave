using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthPolicy : IEnable
{
    public abstract int CalculateDamage(int damage);

    public abstract int CalculateHeal(int healPoints);

    public abstract void OnEnable();

    public abstract void OnDisable();

    protected abstract int CalculateMaxHealth();
}
