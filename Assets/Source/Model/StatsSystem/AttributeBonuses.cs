using System;

public class AttributeBonuses
{
    public event Action SpeedChanged;
    public event Action HealthChanged;
    public event Action Armorhanged;
    public event Action MagicArmorChanged;

    public float Speed => _runSpeedRate * _characterStats.Dexterity.Value;
    public float Health => _healthRate * _characterStats.Vitality.Value;
    public float Armor => _armorRate * _characterStats.Strength.Value;
    public float MagicArmor => _magicArmorRate * _characterStats.Intellect.Value;
    public float SelfHealing => _characterStats.Intellect.Value * _selfHealinglfRate;
    public float Damage => _characterStats.Strength.Value * _damageRate;
    public float PhysicalResist => CalculateRateDecline(Armor);
    public float MagicResist => CalculateRateDecline(MagicArmor);

    private Stats _characterStats;
    private const float _runSpeedRate = 0.02f;
    private const float _healthRate = 10;
    private const float _armorRate = 0.06f;
    private const float _magicArmorRate = 0.06f;
    private const float _selfHealinglfRate = 0.02f;
    private const float _damageRate = 0.4f;
    private const float _resistRate = 0.06f;

    public AttributeBonuses(Stats characterStats)
    {
        _characterStats = characterStats;

        _characterStats.Dexterity.ValueChanged += OnSpeedChanged;
        _characterStats.Vitality.ValueChanged += OnHealthChanged;
        _characterStats.Strength.ValueChanged += OnArmorChanged;
        _characterStats.Intellect.ValueChanged += OnMagicArmorChanged;
    }

    ~AttributeBonuses()
    {
        _characterStats.Dexterity.ValueChanged -= OnSpeedChanged;
        _characterStats.Vitality.ValueChanged -= OnHealthChanged;
        _characterStats.Strength.ValueChanged -= OnArmorChanged;
        _characterStats.Intellect.ValueChanged -= OnMagicArmorChanged;
    }

    private void OnSpeedChanged()
    {
        SpeedChanged?.Invoke();
    }

    private void OnHealthChanged()
    {
        HealthChanged?.Invoke();
    }

    private void OnArmorChanged()
    {
        Armorhanged?.Invoke();
    }

    private void OnMagicArmorChanged()
    {
        MagicArmorChanged?.Invoke();
    }

    private float CalculateRateDecline(float ratePoints)
    {
        return _resistRate * ratePoints / (1 + _resistRate * ratePoints);
    }
}
