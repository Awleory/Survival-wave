using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BulletConfig))]
public class BulletPresenter : Presenter<Bullet>
{
    private BulletConfig _bulletConfig;

    public void Initialize(Vector2 direction)
    {
        base.Initialize(GetBullet(direction));
    }

    protected virtual void Awake()
    {
        _bulletConfig = GetComponent<BulletConfig>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Moved += OnMoved;
        Model.Destroyed += Destroy;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Moved -= OnMoved;
        Model.Destroyed -= Destroy;
    }

    private Bullet GetBullet(Vector2 direction)
    {
        return new Bullet(_bulletConfig, direction, transform.position);
    }

    private void OnMoved()
    {
        transform.position = Model.Movement.Position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyTag>(out EnemyTag enemyTag))
            Model.ProcessCollision(enemyTag.Model);
    }
}
