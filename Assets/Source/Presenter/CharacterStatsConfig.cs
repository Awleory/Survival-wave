using UnityEngine;

public class CharacterStatsConfig : MonoBehaviour
{
    [Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    [SerializeField] private int _baseLevel = Config.MinCharacterLevel;
    [SerializeField, Min(1f)] private float _baseHealth = 100;
    [SerializeField, Min(0f)] private float _baseSpeed = 10;

    [SerializeField, Min(1f)] private float _baseVitality = 1f;
    [SerializeField, Min(1f)] private float _baseStrength = 1f;
    [SerializeField, Min(1f)] private float _baseDexterity = 1f;
    [SerializeField, Min(1f)] private float _baseIntellect = 1f;

    [SerializeField, Min(0f)] private float _deltaVitalityPerLevel;
    [SerializeField, Min(0f)] private float _deltaStrengthPerLevel;
    [SerializeField, Min(0f)] private float _deltaDexterityPerLevel;
    [SerializeField, Min(0f)] private float _deltaIntellectPerLevel;

    public float BaseHealth => _baseHealth;
    public float BaseSpeed => _baseSpeed;
    public int BaseLevel => _baseLevel;
    public float BaseVitality => _baseVitality;
    public float BaseStrength => _baseStrength;
    public float BaseIntellect => _baseIntellect;
    public float BaseDexterity => _baseDexterity;
    public float DeltaVitality => _deltaVitalityPerLevel;
    public float DeltaStrength => _deltaStrengthPerLevel;
    public float DeltaIntellect => _deltaIntellectPerLevel;
    public float DeltaDexterity => _deltaDexterityPerLevel;
}
