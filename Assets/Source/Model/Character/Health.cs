using System;

public class Health : IStartable
{
    public event Action Died;
    public event Action<float> ValueChanged;

    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public bool IsAlive => Value > 0;

    public void Initialize(float baseValue)
    {
        Value = baseValue;
        MaxValue = baseValue;
    }

    public void Start()
    {
        ValueChanged?.Invoke(0);
    }

    public void ApplyDamage(float damage, float damageResist, bool isPure)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        damage = CalculateDamage(damage, damageResist, isPure);

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

    public void ResizeHealth(float newMaxValue)
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

    private float CalculateDamage(float damage, float damageResist, bool isPure)
    {
        if (isPure)
            return damage;
        else
            return damage - damage * damageResist;
    }

    protected float CalculateHeal(float healPoitns)
    {
        return healPoitns;
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
