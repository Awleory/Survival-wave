using System.Collections;
using UnityEngine;

public class EnemySpawnerPresenter : Presenter<EnemySpawner>
{
    [SerializeField] private EnemiesFactory _factory;
    [SerializeField] private Transform _spawnedEnemiesParent;
    [SerializeField, Min(0)] private int _maxEnemies = 1;
    [SerializeField, Min(0)] private float _delaySpawn;
    [SerializeField, Min(0)] private float _spawnLineRadius;
    [SerializeField, Min(1)] private int _spawnPointsCount;
    [SerializeField] private bool _enable;

    private EnemySpawner _enemySpawner;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _delay;
    private Player _player;

    public void Initialize(Player target)
    {
        _enemySpawner = new EnemySpawner();
        base.Initialize(_enemySpawner);

        _enemySpawner.Initialize(target, _spawnLineRadius, _spawnPointsCount);

        _player = target;

        _factory.Initialize(_spawnedEnemiesParent);

        _delay = new WaitForSeconds(_delaySpawn);
    }

    protected override void Update()
    {
        base.Update();
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(SpawnCoroutine(_delay));
    }

    public override void EndInitialize()
    {
        if (_enable)
            base.EndInitialize();
    }

    private IEnumerator SpawnCoroutine(WaitForSeconds delay)
    {
        yield return delay;
        while(_spawnedEnemiesParent.childCount < _maxEnemies)
        {
            SpawnRandomSimpleEnemy();
            yield return delay;
        }
        _spawnCoroutine = null;
    }

    private void SpawnRandomSimpleEnemy()
    {
        var enemyModel = _enemySpawner.CreateSimpleEnemy();
        _factory.CreateRandomEnemy(enemyModel, _player.Stats.Level, _enemySpawner.GetRandomSpawnPoint());
    }
}
