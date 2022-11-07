using System;

public class Character : IUpdateble, IEnable
{
    public event Action Moved;
    public Movement Movement => _movement;
    public Stats Stats => _stats;
    public AttributeBonuses AttributeBonuses => _attributeBonuses;
    public Health Health => _health;

    private Movement _movement;
    private CharacterHealth _health;
    private Stats _stats;
    private AttributeBonuses _attributeBonuses;

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
        _movement.Initialize(statsConfig.BaseSpeed, _attributeBonuses);
    }

    public virtual void OnEnable()
    {
        _health.Died += OnDied;
        _health.ValueChanged += OnHealthChanged;
        _movement.Moved += OnMoved;
    }

    public virtual void OnDisable()
    {
        _health.Died -= OnDied;
        _health.ValueChanged -= OnHealthChanged;
        _movement.Moved -= OnMoved;
    }

    public virtual void Update(float deltaTime) 
    {
        _movement.Update(deltaTime);
    }

    public void ApplyDamage(int damage, DamageType type)
    {
        _health.ApplyDamage(damage, type);
    }

    public void ApplyHeal(int healPoints, bool isPure = false)
    {
        _health.ApplyHeal(healPoints, isPure);
    }
   
    private void OnDied()
    {
    }

    private void OnHealthChanged(float difference)
    {
    }

    private void OnMoved()
    {
        Moved.Invoke();
    }
}
