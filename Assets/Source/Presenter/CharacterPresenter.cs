using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
[RequireComponent (typeof(BoxCollider2D))]
public class CharacterPresenter<TModel> : Presenter<TModel> where TModel : Character
{
    [SerializeField] private HealthBarUI _healthBarUI;

    private const string _runParameter = "Run";

    public override void Initialize(TModel model)
    {
        base.Initialize(model);
        Model.Initialize(GetComponent<CharacterStatsConfig>());

        _healthBarUI?.Initialize(Model.Health);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Movement.Moved += OnMoved;
        Model.Movement.StoppedMove += OnStoppedMove;
        Model.Died += OnDied;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Movement.Moved += OnMoved;
        Model.Movement.StoppedMove -= OnStoppedMove;
        Model.Died -= OnDied;
    }

    private void OnMoved()
    {
        transform.position = Model.Movement.Position;
        AnimationController.OnMoved(_runParameter, Model.Movement.Direction);
    }

    private void OnStoppedMove()
    {
        AnimationController.OnStoppedMove(_runParameter);
    }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }
}
