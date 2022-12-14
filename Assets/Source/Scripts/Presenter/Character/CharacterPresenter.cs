using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(AudioSource))]
public class CharacterPresenter<TModel> : Presenter<TModel> where TModel : Character
{
    [SerializeField] private FillBarUI _healthBarUI;
    [SerializeField] private AudioClip _gotDamageSound;

    private AudioSource _audioSource;

    public AnimationController AnimationController { get; private set; }

    private CharacterStatsConfig _statsConfig;

    public virtual void Initialize(TModel model, int level = 0)
    {
        base.Initialize(model);

        _statsConfig = GetComponent<CharacterStatsConfig>();

        Model.Initialize(_statsConfig, level, transform.position);
    }

    public Range GetMinMaxLevel()
    {
        if (_statsConfig == null)
            _statsConfig = GetComponent<CharacterStatsConfig>();

        return new Range(_statsConfig.MinLevel, _statsConfig.MaxLevel);
    }

    protected virtual void Awake()
    {
        AnimationController = GetComponent<AnimationController>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = Config.Volume;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Movement.Moved += OnMoved;
        Model.Movement.StoppedMove += OnStoppedMove;
        Model.Died += OnDying;
        Model.Destroyed += Destroy;
        Model.Health.ValueChanged += OnHealthChanged;
        Model.GotDamage += OnGotDamage;

        AnimationController.DeathAnimEnded += OnDied;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Movement.Moved -= OnMoved;
        Model.Movement.StoppedMove -= OnStoppedMove;
        Model.Died -= OnDying;
        Model.Destroyed -= Destroy;
        Model.Health.ValueChanged -= OnHealthChanged;
        Model.GotDamage -= OnGotDamage;

        AnimationController.DeathAnimEnded -= OnDied;
    }

    public virtual void Respawn(Vector2 positon, bool restoreHealth = true, int level = 0)
    {
        Model.Respawn(positon, restoreHealth, level);
        AnimationController.OnAlive();
    }

    private void OnMoved()
    {
        transform.position = Model.Movement.Position; 

        AnimationController.OnMoved(Model.Movement.Direction);
    }

    private void OnStoppedMove()
    {
        AnimationController.OnStoppedMove();
    }

    private void OnHealthChanged(float delta)
    {
        _healthBarUI.UpdateValue(Model.Health.Value, Model.Health.MaxValue);
    }

    private void OnGotDamage()
    {
        AnimationController.OnGotHit();
        _audioSource.PlayOneShot(_gotDamageSound);
    }

    protected virtual void OnDying()
    {
        AnimationController.OnDying();
    }

    protected virtual void OnDied() 
    {
        Destroy();
    }
}
