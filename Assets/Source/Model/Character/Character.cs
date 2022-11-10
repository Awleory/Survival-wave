
public class Character : IUpdateble, IEnable
{
    public Movement Movement => _movement;
    public Stats Stats => _stats;
    public AttributeBonuses AttributeBonuses => _attributeBonuses;
    public Health Health => _health;
    public float SpeedAttack { get; private set; }
    public float Damage => _attributeBonuses.Damage + _baseDamage;

    private Movement _movement;
    private CharacterHealth _health;
    private Stats _stats;
    private AttributeBonuses _attributeBonuses;
    private float _baseDamage;

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
        _baseDamage = statsConfig.BaseDamage;
    }

    public virtual void OnEnable()
    {
        _health.Died += OnDied;
        _health.ValueChanged += OnHealthChanged;
    }

    public virtual void OnDisable()
    {
        _health.Died -= OnDied;
        _health.ValueChanged -= OnHealthChanged;
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
    }

    private void OnHealthChanged(float difference)
    {
    }
}
