using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
public class CharacterPresenter<TModel> : MonoBehaviour where TModel : Character
{
    [SerializeField] private HealthBarUI _healthBarUI;

    public TModel Model => _model;

    private TModel _model;
    private IUpdateble _updateble = null;
    private IEnable _enable = null;

    public virtual void Initialize(TModel model)
    {
        _model = model;
        _model.Initialize(GetComponent<CharacterStatsConfig>());

        _healthBarUI.Initialize(_model.Health);

        if (_model is IUpdateble)
            _updateble = (IUpdateble)_model;

        if (_model is IEnable)
            _enable = (IEnable)_model;

        enabled = true;
    }

    private void OnEnable()
    {
        _enable?.OnEnable();
        _model.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _enable?.OnDisable();
        _model.Moved -= OnMoved;
    }

    private void Update() => _updateble?.Update(Time.deltaTime);

    private void OnMoved()
    {
        transform.position = _model.Movement.Position;
    }
}
