using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private bool _flipX;
    [SerializeField] private bool _flipY;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMoved(string moveParameter, Vector2 direction)
    {
        if (direction.x != 0)
            SetFlipX(direction.x < 0);

        _animator.SetBool(moveParameter, true);
    }

    public void OnStoppedMove(string moveParameter)
    {
        _animator.SetBool(moveParameter, false);
    }

    public void OnRotated(Vector2 direction)
    {
        if (direction.x != 0)
            SetFlipY(direction.x < 0);
    }

    private void SetFlipX(bool state)
    {
        _spriteRenderer.flipX = _flipX ? !state : state;
    }

    private void SetFlipY(bool state)
    {
        _spriteRenderer.flipY = _flipY ? !state : state;
    }
}
