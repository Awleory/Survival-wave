using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private bool _flipX;

    public Animator Animator => _animator;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Character _model;

    private const string _runParameter = "Run";

    public void Initialize(Character model)
    {
        _model = model;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        enabled = true;
    }

    private void OnEnable()
    {
        _model.Movement.StoppedMove += OnStoppedMove;
        _model.Movement.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _model.Movement.StoppedMove -= OnStoppedMove;
        _model.Movement.Moved -= OnMoved;
    }

    private void SetFlipX(bool state)
    {
        _spriteRenderer.flipX = _flipX ? !state : state;
    }

    private void OnStoppedMove()
    {
        _animator.SetBool(_runParameter, false);
    }

    private void OnMoved()
    {
        if (_model.Movement.Direction.x != 0)
            SetFlipX(_model.Movement.Direction.x < 0);

        _animator.SetBool(_runParameter, true);
    }
}
