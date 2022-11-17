using UnityEngine;

public class WeaponPresenter : Presenter<Weapon>
{
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private BulletPresenter _bulletPresenter;
    [SerializeField] private Transform _gunPoint;

    private Vector2 _direction;

    public override void Initialize(Weapon weapon)
    {
        base.Initialize(weapon);

        Model.Initialize(_shootingSpeed, transform.position);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Shot += OnShot;
        Model.Rotated += OnRotated;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Shot -= OnShot;
        Model.Rotated -= OnRotated;
    }

    private void OnShot()
    {
        BulletPresenter bulletPresenter = Instantiate(_bulletPresenter, _gunPoint.position, Quaternion.identity);
        bulletPresenter.Initialize(_direction); Debugger.UpdateText("_direction", _direction);
        bulletPresenter.EndInitialize();
    }

    private void OnRotated(Vector2 screenMousePosition)
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
        float angle = Vector2.SignedAngle(Vector2.right, worldMousePosition - transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        _direction = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));

        AnimationController.OnRotated(_direction);
    }
}
