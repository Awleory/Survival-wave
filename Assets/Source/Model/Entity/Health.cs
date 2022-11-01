using System;
using UnityEngine.SocialPlatforms;

public class Health : IEnable
{
    public event Action Died;
    public event Action<int> ValueChanged;

    public int Value { get; private set; }
    public int MaxValue { get; private set; }
    public bool IsAlive => Value > 0;

    private HealthPolicy _healthPolicy;

    public Health(HealthPolicy healthPolicy)
    {
        Value = MaxValue;
        _healthPolicy = healthPolicy;
    }

    public void OnEnable()
    {
        _healthPolicy.MaxValueChanged += OnMaxValueChanged;
    }

    public void OnDisable()
    {
        _healthPolicy.MaxValueChanged -= OnMaxValueChanged;
    }

    public void OnMaxValueChanged(int maxValue)
    {
        ResizeHealth(maxValue);
    }

    public void ApplyDamage(int damage, bool isPure = false)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        if (isPure == false)
            damage = _healthPolicy.CalculateDamage(damage);

        Value = Math.Max(Value - damage, 0);

        ValueChanged?.Invoke(-damage);
        TryDie();
    }

    public void ApplyHeal(int healPoints, bool isPure = false)
    {
        if (healPoints < 0)
            throw new ArgumentOutOfRangeException(nameof(healPoints));

        if (isPure == false)
            healPoints = _healthPolicy.CalculateHeal(healPoints);

        Value = Math.Min(Value + healPoints, MaxValue);

        ValueChanged?.Invoke(healPoints);
    }

    public void Kill()
    {
        int tempValue = Value;
        Value = 0;

        ValueChanged?.Invoke(-tempValue);
        TryDie();
    }

    public void Restore()
    {
        int tempValue = MaxValue - Value;
        Value = MaxValue;

        ValueChanged?.Invoke(tempValue);
    }

    private void ResizeHealth(int newMaxValue)
    {
        if (newMaxValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(newMaxValue));

        if (Value == 0 || MaxValue == 0)
        {
            MaxValue = newMaxValue;
        }
        else
        {
            Value = Value * newMaxValue / MaxValue;
            MaxValue = newMaxValue;
        }

        ValueChanged?.Invoke(Value);
    }

    private bool TryDie()
    {
        if (IsAlive)
        {
            return false;
        }
        else
        {
            Died.Invoke();
            return true;
        }
    }
}
