using System;

public class AttributeBonuses
{
    public event Action Changed;

    public float Speed => CalculateBonus(_characterStats.Dexterity.Value, _runSpeedLimit, _runGrowthRate, _runSpeedRate);
    public float AttackSpeed => CalculateBonus(_characterStats.Dexterity.Value, _attackSpeedLimit, _attackGrowthRate, _attackSpeedRate);
    public float Health => _healthRate * _characterStats.Vitality.Value;
    public float SelfHealing => CalculateBonus(_characterStats.Intellect.Value, _selfHealinglLimit, _selfHealinglGrowthRate, _selfHealinglfRate);
    public float Damage => CalculateBonus(_characterStats.Strength.Value, _damageLimit, _damageGrowthRate, _damageRate);
    public float PhysicalResist => CalculateResist(_characterStats.Strength.Value, _resistGrowthRate, _physicalResistRate);
    public float MagicResist => CalculateResist(_characterStats.Intellect.Value, _resistGrowthRate, _magicResistRate);

    private Stats _characterStats;

    private const float _physicalResistRate = 0.06f;
    private const float _magicResistRate = 0.06f;
    private const float _resistGrowthRate = 2f;

    private const float _damageRate = 0.4f;
    private const float _damageLimit = 5f;
    private const float _damageGrowthRate = 0.02f;

    private const float _selfHealinglfRate = 0.02f;
    private const float _selfHealinglLimit = 5;
    private const float _selfHealinglGrowthRate = 0.03f;

    private const float _healthRate = 0.4f;

    private const float _attackSpeedRate = 0.05f;
    private const float _attackSpeedLimit = 5;
    private const float _attackGrowthRate = 5;

    private const float _runSpeedRate = 0.05f;
    private const float _runSpeedLimit = 5f;
    private const float _runGrowthRate = 5f;

    public AttributeBonuses(Stats characterStats)
    {
        _characterStats = characterStats;

        _characterStats.Updated += OnUpdated;
    }

    ~AttributeBonuses()
    {
        _characterStats.Updated -= OnUpdated;
    }

    private void OnUpdated()
    {
        Changed?.Invoke();
    }

    private float CalculateResist(float value, float growthRate, float valueRate)
    {
        return CalculateBonus(value, 1, growthRate, valueRate) - 1;
    }

    private float CalculateBonus(float value, float limit, float growthRate, float valueRate)
    {
        return limit * (valueRate * value / (growthRate + valueRate * value)) + 1;
    }
}
