using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BulletConfig))]
public class BulletPresenter : Presenter<Bullet>
{
    public BulletConfig Config { get; private set; }

    private WaitForSeconds _lifeTime;
    private Coroutine _lifeTimeCoroutine;

    public void Initialize(Vector2 direction)
    {
        Config = GetComponent<BulletConfig>();
        _lifeTime = new WaitForSeconds(Config.LifeTime);

        base.Initialize(GetBullet(direction));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Moved += OnMoved;
        Model.Destroyed += Destroy;

        if (_lifeTimeCoroutine != null)
            StopCoroutine(_lifeTimeCoroutine);

        _lifeTimeCoroutine = StartCoroutine(ProcessLiveTime());
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Moved -= OnMoved;
        Model.Destroyed -= Destroy;
    }

    public void Respawn(Vector2 position, Quaternion rotation, Vector2 direction)
    {
        transform.rotation = rotation;
        Model.Reset(position, direction);
    }

    private Bullet GetBullet(Vector2 direction)
    {
        return new Bullet(Config, direction, transform.position);
    }

    private void OnMoved()
    {
        transform.position = Model.Movement.Position;
    }

    private IEnumerator ProcessLiveTime()
    {
        yield return _lifeTime;
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyTag>(out EnemyTag enemyTag))
            Model.ProcessCollision(enemyTag.Model);
    }
}
