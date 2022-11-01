using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
public class CharacterPresenter<T> : EntityPresenter<T> where T : Character
{
    [Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    [SerializeField] private int _startLevel = Config.MinCharacterLevel;

    private CharacterStatsConfig _statsConfig;

    public override void Initialize(T model)
    {
        base.Initialize(model);
         
        _statsConfig = GetComponent<CharacterStatsConfig>();
        _statsConfig.Initialize(_startLevel);

        Model.Stats.Initialize(_statsConfig, _startLevel);
    }
}
