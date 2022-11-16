using System;

public class CharacterHealth : Health
{
    private AttributeBonuses _attributeBonuses;

    ~CharacterHealth()
    {
        _attributeBonuses.Changed -= OnMaxValueChanged;
    }

    public void Initialize(AttributeBonuses attributeBonuses)
    {
        _attributeBonuses = attributeBonuses;
        _attributeBonuses.Changed += OnMaxValueChanged;
        OnMaxValueChanged();
    }

    protected override float CalculateDamage(float damage, DamageType type)
    {
        switch (type)
        {
            case DamageType.Pure:
                return damage;
            case DamageType.Magic:
                return damage - damage * _attributeBonuses.MagicResist;
            case DamageType.Physical:
                return damage - damage * _attributeBonuses.PhysicalResist;
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }

    protected override float CalculateHeal(float healPoitns)
    {
        return healPoitns + _attributeBonuses.SelfHealing * healPoitns;
    }

    private void OnMaxValueChanged()
    {
        ResizeHealth(_attributeBonuses.Health);
    }
}
