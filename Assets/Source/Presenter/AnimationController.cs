using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private bool _flipX;
    [SerializeField] private bool _flipY;

    public event Action DeathAnimEnded;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string _runParameter = "Run";
    private const string _deathParameter = "Death";
    private const string _hitParameter = "Hit";
    private const string _attackParameter = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMoved(Vector2 direction)
    {
        if (direction.x != 0)
            SetFlipX(direction.x < 0);

        _animator.SetBool(_runParameter, true);
    }

    public void OnStoppedMove()
    {
        _animator.SetBool(_runParameter, false);
    }

    public void OnRotated(Vector2 direction)
    {
        if (direction.x != 0)
            SetFlipY(direction.x < 0);
    }

    public void OnDying()
    {
        _animator.SetTrigger(_deathParameter);
    }

    public void OnGotHit()
    {
        _animator.SetTrigger(_hitParameter);
    }

    public void OnDeadAnimEnded() => DeathAnimEnded?.Invoke();

    private void SetFlipX(bool state)
    {
        _spriteRenderer.flipX = _flipX ? !state : state;
    }

    public void OnAttacked()
    {
        _animator?.SetTrigger(_attackParameter);
    }

    private void SetFlipY(bool state)
    {
        _spriteRenderer.flipY = _flipY ? !state : state;
    }
}
