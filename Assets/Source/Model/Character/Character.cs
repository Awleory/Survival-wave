using System;
using UnityEngine;

public class Character : IUpdateble, IStartable, IEnable
{
    public event Action Died;

    public Movement Movement => _movement;
    public Stats Stats => _stats;
    public AttributeBonuses AttributeBonuses => _attributeBonuses;
    public Health Health => _health;
    public float SpeedAttack => _baseAttackSpeed * _attributeBonuses.AttackSpeed;
    public float AttacksPerSecond => 1 / SpeedAttack;
    public float Damage => _attributeBonuses.Damage + _baseDamage;

    private Movement _movement;
    private CharacterHealth _health;
    private Stats _stats;
    private AttributeBonuses _attributeBonuses;
    private float _baseDamage;
    private float _baseAttackSpeed;

    public Character()
    {
        _movement = new Movement();
        _health = new CharacterHealth();
        _stats = new Stats();
        _attributeBonuses = new AttributeBonuses(_stats);
    }

    public void Initialize(CharacterStatsConfig statsConfig)
    {
        _stats.Initialize(statsConfig, statsConfig.BaseLevel);
        _health.Initialize(statsConfig.BaseHealth);
        _health.Initialize(_attributeBonuses);
        _movement.Initialize(statsConfig.BaseSpeed, Vector2.zero);
        _baseDamage = statsConfig.BaseDamage;
        _baseAttackSpeed = statsConfig.BaseAttackSpeed;
    }

    public virtual void Start()
    {
        _stats.Start();
    }

    public virtual void OnEnable()
    {
        _health.Died += OnDied;
        _health.ValueChanged += OnHealthChanged;
        _attributeBonuses.Changed += OnAttributeBonusesUpdated;
    }

    public virtual void OnDisable()
    {
        _health.Died -= OnDied;
        _health.ValueChanged -= OnHealthChanged;
        _attributeBonuses.Changed -= OnAttributeBonusesUpdated;
    }

    public virtual void Update(float deltaTime) 
    {
        _movement.Update(deltaTime);
    }

    public void ApplyDamage(float damage, DamageType type)
    {
        _health.ApplyDamage(damage, type);
    }

    public void ApplyHeal(float healPoints, bool isPure = false)
    {
        _health.ApplyHeal(healPoints, isPure);
    }
   
    private void OnDied()
    {
        Died?.Invoke();
    }

    private void OnHealthChanged(float difference)
    {
    }

    protected virtual void OnAttributeBonusesUpdated() 
    {
        _movement.OnBonusSpeedChanged(_attributeBonuses.Speed);
    }
}
