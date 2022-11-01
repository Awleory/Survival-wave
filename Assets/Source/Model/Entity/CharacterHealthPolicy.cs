using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthPolicy : HealthPolicy
{
    public event Action<int> MaxValueChanged;

    private StatAttribute _characterVitality;

    private const float _healthRate = 10;

    public void Initialize(StatAttribute characterVitality)
    {
        _characterVitality = characterVitality;
    }

    public override int CalculateDamage(int damage)
    {
        return damage;
    }

    public override int CalculateHeal(int healPoitns)
    {
        return healPoitns;
    }

    public override void OnEnable()
    {
        _characterVitality.ValueChanged += OnVitalityChanged;
    }

    public override void OnDisable()
    {
        _characterVitality.ValueChanged -= OnVitalityChanged;
    }

    private void OnVitalityChanged()
    {
        MaxValueChanged?.Invoke(CalculateMaxHealth());
    }

    protected override int CalculateMaxHealth()
    {
        return (int)(_characterVitality.Value * _healthRate);
    }
}
