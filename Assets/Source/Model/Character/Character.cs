
public class Character<THealth> : Entity<THealth> where THealth : CharacterHealth
{
    private CharacterStats _stats;
    private AttributeBonuses _attributeBonuses;

    public Character(THealth characterHealth) : base (characterHealth)
    {
        _stats = new CharacterStats();
        _attributeBonuses = new AttributeBonuses(_stats);
    }

    public void Initialize(CharacterStatsConfig statsConfig, int startLevel)
    {
        _stats.Initialize(statsConfig, startLevel);
        _health.Initialize(_attributeBonuses);
    }
}
