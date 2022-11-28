using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Presenter<TModel> : MonoBehaviour where TModel : class
{
    public TModel Model { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; } 

    private IUpdateble _updateble = null;
    private IEnable _enable = null;
    private IStartable _startable = null;

    public virtual void Initialize(TModel model)
    {
        Model = model;

        if (Model is IUpdateble)
            _updateble = (IUpdateble) Model;

        if (Model is IEnable)
            _enable = (IEnable)Model;

        if (Model is IStartable)
            _startable = (IStartable)Model;

        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void EndInitialize()
    {
        enabled = true;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void Destroy(Character character)
    {
        Destroy();
    }

    protected virtual void Start()
    {
        _startable?.Start();
    }

    protected virtual void Update()
    {
        _updateble?.Update(Time.deltaTime);
    }

    protected virtual void OnEnable()
    {
        _enable?.OnEnable();
    }

    protected virtual void OnDisable()
    {
        _enable?.OnDisable();
    }
}
