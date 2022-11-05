using System;

public class CharacterHealth : Health
{
    private AttributeBonuses _attributeBonuses;

    ~CharacterHealth()
    {
        _attributeBonuses.HealthChanged -= OnMaxValueChanged;
    }

    public void Initialize(AttributeBonuses attributeBonuses)
    {
        _attributeBonuses = attributeBonuses;
        _attributeBonuses.HealthChanged += OnMaxValueChanged;
    }

    protected override float CalculateDamage(float damage, DamageType type)
    {
        switch (type)
        {
            case DamageType.Pure:
                return damage;
            case DamageType.Magic:
                damage -= Math.Max(_attributeBonuses.MagicArmor, 0);
                return damage;
            case DamageType.Physical:
                damage -= Math.Max(_attributeBonuses.Armor, 0);
                return damage;
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
