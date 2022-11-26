using System;
using UnityEngine;

public abstract class Character : IUpdateble, IStartable, IEnable
{
    public event Action Died;
    public event Action GotDamage;
    public event Action<Character> Destroyed;

    public Movement Movement => _movement;
    public Stats Stats => _stats;
    public Health Health => _health;
    public float AttacksPerSecond => _stats.AttacksPerSecond;
    public float Damage => _stats.Attack.Value;
    public bool IsAlive => _health.IsAlive;

    private Movement _movement;
    private Health _health;
    private Stats _stats;

    public Character()
    {
        _movement = new Movement();
        _health = new Health();
        _stats = new Stats();
    }

    ~Character()
    {
        Destroyed?.Invoke(this);
    }

    public void Initialize(CharacterStatsConfig statsConfig, int level, Vector2 startPosition)
    {
        int startLevel = level == 0 ? statsConfig.BaseLevel : level;
        _stats.Initialize(statsConfig, startLevel);
        _health.Initialize(statsConfig.BaseHealth);
        _movement.Initialize(startPosition);
    }

    public virtual void Start()
    {
        _stats.Start();
        _health.Start();
    }

    public virtual void OnEnable()
    {
        _health.Died += OnDied;
        _stats.Health.ValueChanged += OnHealthAttributeChanged;
        _stats.MoveSpeed.ValueChanged += OnRunSpeedAttributeChanged;
    }

    public virtual void OnDisable()
    {
        _health.Died -= OnDied;
        _stats.Health.ValueChanged += OnHealthAttributeChanged;
        _stats.MoveSpeed.ValueChanged -= OnRunSpeedAttributeChanged;
    }

    public virtual void Update(float deltaTime) 
    {
        _movement.Update(deltaTime);
    }

    public void ApplyDamage(float damage, bool isPure)
    {
        if (_health.IsAlive == false)
            return;

        _health.ApplyDamage(damage, Stats.DamageResist, isPure);
        GotDamage?.Invoke();
    }

    public void ApplyHeal(float healPoints, bool isPure = false)
    {
        _health.ApplyHeal(healPoints, isPure);
    }
   
    public void TakeExp(int expPoints)
    {
        _stats.TakeExp(expPoints);
    }

    public void Kill()
    {
        _health.Kill();
    }

    public void Respawn(Vector2 spawnPoint, bool restoreHealth = true, int level = 0)
    {
        Movement.SetPosition(spawnPoint);
        if (restoreHealth)
            _health.Restore();

        _movement.UnFreeze();

        if (level > 0)
            _stats.SetLevel(level);
    }

    public void Destroy()
    {
        Destroyed?.Invoke(this);
    }

    protected virtual void OnDied()
    {
        _movement.Freeze();
        Died?.Invoke();
    }

    private void OnHealthAttributeChanged() => _health.ResizeHealth(_stats.Health.Value);

    private void OnRunSpeedAttributeChanged() => _movement.OnSpeedChanged(_stats.MoveSpeed.Value);
}
