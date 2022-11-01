using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
public class CharacterPresenter<TCharacter, THealthPolicy> : 
    EntityPresenter<TCharacter, THealthPolicy> where TCharacter : Character where THealthPolicy : CharacterHealthPolicy
{
    [Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    [SerializeField] private int _startLevel = Config.MinCharacterLevel;

    private CharacterStatsConfig _statsConfig;

    public override void Initialize(TCharacter model, THealthPolicy healthPolicy)
    {
        _statsConfig = GetComponent<CharacterStatsConfig>();
        _statsConfig.Initialize(_startLevel);
        model.Stats.Initialize(_statsConfig, _startLevel);

        healthPolicy.Initialize(model.Stats.Vitality);

        base.Initialize(model, healthPolicy);
    }
}
