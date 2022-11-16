using UnityEngine;

public class WeaponPresenter : Presenter<Weapon>
{
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private BulletPresenter _bulletPresenter;
    [SerializeField] private Transform _gunPoint;

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
        BulletPresenter bullet = Instantiate(_bulletPresenter, _gunPoint.position, Quaternion.identity);
        bullet.Initialize(Model.Direction);
        bullet.EndInitialize();
    }

    private void OnRotated()
    {
        float angle = Vector2.SignedAngle(Vector2.right, Model.Direction);
        transform.eulerAngles = new Vector3(0f, 0f, angle);

        AnimationController.OnRotated(Model.Direction);
    }
}
