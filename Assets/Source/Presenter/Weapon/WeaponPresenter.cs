using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(BulletsFactory))]
public class WeaponPresenter : Presenter<Weapon>
{
    [SerializeField] private float _shootsPerSecond;
    [SerializeField] private BulletPresenter _bulletPresenter;
    [SerializeField] private Transform _gunPoint;
    
    private BulletsFactory _bulletsFactory;

    public AnimationController AnimationController { get; private set; }

    private Vector2 _direction;

    public virtual void Initialize(Weapon weapon, Transform bulletsCantainer)
    {
        base.Initialize(weapon);

        _bulletsFactory = GetComponent<BulletsFactory>();
        _bulletsFactory.Initialize(_bulletPresenter, bulletsCantainer, _shootsPerSecond);

        Model.Initialize(_shootsPerSecond, transform.position);
    }

    public void Hide()
    {
        enabled = false;
        SpriteRenderer.enabled = false;
    }

    public void Show()
    {
        SpriteRenderer.enabled = true;
        enabled = true;
    }

    private void Awake()
    {
        AnimationController = GetComponent<AnimationController>();
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
        if(_bulletsFactory.TryGetBullet(_bulletPresenter, out BulletPresenter bulletPresenter, _direction))
        {
            Quaternion rotation = _bulletPresenter.transform.rotation * Quaternion.Euler(transform.eulerAngles);
            bulletPresenter.Respawn(_gunPoint.position, rotation, _direction);
            bulletPresenter.gameObject.SetActive(true);
        }
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
