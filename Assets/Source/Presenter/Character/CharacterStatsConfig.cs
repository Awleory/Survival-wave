using UnityEngine;

public class CharacterStatsConfig : MonoBehaviour
{
    [Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    [SerializeField] private int _minLevel = Config.MinCharacterLevel;
    [Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    [SerializeField] private int _maxLevel = Config.MaxCharacterLevel;
    [Range(Config.MinCharacterLevel, Config.MaxCharacterLevel)]
    [SerializeField] private int _baseLevel = Config.MinCharacterLevel;

    [SerializeField, Min(1)] private float _baseHealth = 100;
    [SerializeField, Min(1)] private float _maxHealth = 100;

    [SerializeField, Min(0f)] private float _baseRunSpeed = 1;
    [SerializeField, Range(0, Config.MaxRunSpeed)] private float _maxRunSpeed = 5;

    [SerializeField, Min(0f)] private float _baseDefence = 10;
    [SerializeField, Min(1)] private float _maxDefence = 10;

    [SerializeField, Min(0.1f)] private float _baseAttackSpeed = 1;
    [SerializeField, Range(0.1f, Config.MaxAttackSpeed)] private float _maxAttackSpeed = 5;

    [SerializeField, Min(1f)] private float _baseAttack = 10;
    [SerializeField, Min(1)] private float _maxAttack = 100;

    [SerializeField] private CharacterPresenter<Player> _characterPresenter;
    [SerializeField] private bool TestMode;

    public float BaseHealth => _baseHealth;
    public float MaxHealth => _maxHealth;

    public float BaseRunSpeed => _baseRunSpeed;
    public float MaxRunSpeed => _maxRunSpeed;

    public float BaseAttack => _baseAttack;
    public float MaxAttack => _maxAttack;

    public float BaseDefence => _baseDefence;
    public float MaxDefence => _maxDefence;

    public float BaseAttackSpeed => _baseAttackSpeed;
    public float MaxAttackSpeed => _maxAttackSpeed;

    public int MinLevel => _minLevel;
    public int MaxLevel => _maxLevel;
    public int BaseLevel => _baseLevel;

    public void OnValidate()
    {
        if (TestMode && Application.isPlaying && _characterPresenter != null)
        {
            _characterPresenter.Model.Stats.Initialize(this, _baseLevel);
            _characterPresenter.Model.Stats.Start();
        }

        _maxLevel = Mathf.Max(_maxLevel, _minLevel);
        _minLevel = Mathf.Min(_minLevel, _maxLevel);
        _baseLevel = Mathf.Min(_baseLevel, _maxLevel);
        _baseLevel = Mathf.Max(_baseLevel, _minLevel);
        _baseHealth = Mathf.Min(_baseHealth, _maxHealth);
        _baseDefence = Mathf.Min(_baseDefence, _maxDefence);
        _baseAttackSpeed = Mathf.Min(_baseAttackSpeed, _maxAttackSpeed);
        _baseRunSpeed = Mathf.Min(_baseRunSpeed, _maxRunSpeed);
        _baseAttackSpeed = Mathf.Min(_baseAttackSpeed, _maxAttackSpeed);
    }
}
