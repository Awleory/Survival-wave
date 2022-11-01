using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class EntityPresenter<T> : MonoBehaviour where T : Entity
{
    public T Model => _model;
    
    private T _model;
    private IUpdateble _updateble = null;
    private Rigidbody2D _rigidBody2D;
    private BoxCollider2D _boxCollider2D;

    public virtual void Initialize(T model)
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _model = model;
        _model.Initialize(_boxCollider2D, _rigidBody2D);

        if (_model is IUpdateble)
        {
            _updateble = (IUpdateble)_model;
        }

        enabled = true;
    }

    protected virtual void OnEnable()
    {
        _model.OnEnable();
    }

    protected virtual void OnDisable()
    {
        _model.OnDisable();
    }

    private void Update() => _updateble?.Update(Time.deltaTime);

    private void FixedUpdate() => _updateble?.FixedUpdate(Time.deltaTime);
}
