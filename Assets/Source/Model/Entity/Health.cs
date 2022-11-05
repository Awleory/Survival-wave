using System;

public class Health
{
    public event Action Died;
    public event Action<float> ValueChanged;

    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public bool IsAlive => Value > 0;

    private float _baseValue;

    public void Initialize(float baseValue)
    {
        _baseValue = baseValue;
        Value = baseValue;
        MaxValue = baseValue;
    }

    public void ApplyDamage(float damage, DamageType type)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        damage = CalculateDamage(damage, type);

        Value = Math.Max(Value - damage, 0);

        ValueChanged?.Invoke(-damage);
        TryDie();
    }

    public void ApplyHeal(float healPoints, bool isPure = false)
    {
        if (healPoints < 0)
            throw new ArgumentOutOfRangeException(nameof(healPoints));

        if (isPure == false)
            healPoints = CalculateHeal(healPoints);

        Value = Math.Min(Value + healPoints, MaxValue);

        ValueChanged?.Invoke(healPoints);
    }

    public void Kill()
    {
        float tempValue = Value;
        Value = 0;

        ValueChanged?.Invoke(-tempValue);
        TryDie();
    }

    public void Restore()
    {
        float tempValue = MaxValue - Value;
        Value = MaxValue;

        ValueChanged?.Invoke(tempValue);
    }

    protected virtual float CalculateDamage(float damage, DamageType type)
    {
        return damage;
    }

    protected virtual float CalculateHeal(float healPoitns)
    {
        return healPoitns;
    }

    protected void ResizeHealth(float newBonusValue)
    {
        if (newBonusValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(newBonusValue));

        float newMaxValue = _baseValue + newBonusValue;

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
