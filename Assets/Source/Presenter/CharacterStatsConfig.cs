using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsConfig : MonoBehaviour
{
    [Range(1f, float.MaxValue)]
    [SerializeField] private float _baseVitality = 1f;
    [Range(1f, float.MaxValue)]
    [SerializeField] private float _baseStrength = 1f;
    [Range(1f, float.MaxValue)]
    [SerializeField] private float _baseIntellect = 1f;
    [Range(0f, float.MaxValue)]
    [SerializeField] private float _deltaVitalityPerLevel;
    [Range(0f, float.MaxValue)]
    [SerializeField] private float _deltaStrengthPerLevel;
    [Range(0f, float.MaxValue)]
    [SerializeField] private float _deltaIntellectPerLevel;

    public StatAttribute Vitality { get; private set; }
    public StatAttribute Strength { get; private set; }
    public StatAttribute Intellect { get; private set; }

    public void Initialize(int level)
    {
        Vitality = new StatAttribute(_baseVitality, _deltaVitalityPerLevel, level);
        Strength = new StatAttribute(_baseStrength, _deltaStrengthPerLevel, level);
        Intellect = new StatAttribute(_baseIntellect, _deltaIntellectPerLevel, level);
    }
}
