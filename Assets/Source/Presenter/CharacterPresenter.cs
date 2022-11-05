using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
public class CharacterPresenter<TCharacter> : EntityPresenter<Entity<Health>> 
    where TCharacter : Character<CharacterHealth>
{
    [SerializeField, Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    private int _startLevel = Config.MinCharacterLevel;

    private CharacterStatsConfig _statsConfig;

    public void Initialize(TCharacter model)
    {
        _statsConfig = GetComponent<CharacterStatsConfig>();

        model.Initialize(_statsConfig, _startLevel);
    }
}
