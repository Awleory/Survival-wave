using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsConfig))]
[RequireComponent (typeof(BoxCollider2D))]
public class CharacterPresenter<TModel> : Presenter<TModel> where TModel : Character
{
    [SerializeField] private FillBarUI _healthBarUI;

    private Rigidbody2D _rigidBody2D;

    private const string _runParameter = "Run";

    public override void Initialize(TModel model)
    {
        base.Initialize(model);
        Model.Initialize(GetComponent<CharacterStatsConfig>(), transform.position);
    }

    protected override void Awake()
    {
        base.Awake();

        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Movement.Moved += OnMoved;
        Model.Movement.StoppedMove += OnStoppedMove;
        Model.Died += OnDied;
        Model.Destroyed += Destroy;
        Model.Health.ValueChanged += OnHealthChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Movement.Moved += OnMoved;
        Model.Movement.StoppedMove -= OnStoppedMove;
        Model.Died -= OnDied;
        Model.Destroyed -= Destroy;
        Model.Health.ValueChanged -= OnHealthChanged;
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

    private void OnHealthChanged(float delta)
    {
        _healthBarUI.UpdateValue(Model.Health.Value, Model.Health.MaxValue);
    }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }
}
