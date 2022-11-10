using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
[RequireComponent (typeof(AnimationController))]
[RequireComponent (typeof(BoxCollider2D))]
public class CharacterPresenter<TModel> : MonoBehaviour where TModel : Character
{
    [SerializeField] private HealthBarUI _healthBarUI;

    public Animator Animator { get; private set; }
    public TModel Model => _model;

    private TModel _model;
    private IUpdateble _updateble = null;
    private IEnable _enable = null;

    public virtual void Initialize(TModel model)
    {
        _model = model;
        _model.Initialize(GetComponent<CharacterStatsConfig>());

        GetComponent<AnimationController>().Initialize(_model);

        _healthBarUI.Initialize(_model.Health);

        if (_model is IUpdateble)
            _updateble = (IUpdateble)_model;

        if (_model is IEnable)
            _enable = (IEnable)_model;

        enabled = true;
    }

    protected virtual void OnEnable()
    {
        _enable?.OnEnable();
        _model.Movement.Moved += OnMoved;
    }

    protected virtual void OnDisable()
    {
        _enable?.OnDisable();
        _model.Movement.Moved -= OnMoved;
    }

    protected virtual void Update() => _updateble?.Update(Time.deltaTime);

    private void OnMoved()
    {
        transform.position = _model.Movement.Position;
    }
}
