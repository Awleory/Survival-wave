using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnerPresenter : Presenter<EnemySpawner>
{
    [SerializeField] private EnemiesFactory _factory;
    [SerializeField, Min(0)] private int _maxEnemies = 1;
    [SerializeField, Min(0)] private float _delaySpawn;
    [SerializeField, Min(0)] private float _spawnLineRadius;
    [SerializeField, Min(1)] private int _spawnPointsCount;
    [SerializeField] private Transform _container;
    [SerializeField] private bool _enable;

    private EnemySpawner _enemySpawner;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _delay;
    private Player _player;

    public void Initialize(Player target)
    {
        _player = target;
        _enemySpawner = new EnemySpawner();
        base.Initialize(_enemySpawner);

        _enemySpawner.Initialize(_player, _spawnLineRadius, _spawnPointsCount);
        _factory.Initialize(_maxEnemies, _enemySpawner, _container);

        _delay = new WaitForSeconds(_delaySpawn);
    }

    protected override void Update()
    {
        base.Update();
        if (_spawnCoroutine == null && _enable)
            _spawnCoroutine = StartCoroutine(SpawnCoroutine(_delay));
    }

    public override void EndInitialize()
    {
        if (_enable)
            base.EndInitialize();
    }

    public void Respawn()
    {
        _enemySpawner.Reset();
    }

    private IEnumerator SpawnCoroutine(WaitForSeconds delay)
    {
        yield return delay;
        Spawn();
        _spawnCoroutine = null;
    }

    private void Spawn()
    {
        if (_factory.TryGetRandomEnemy(_player.Stats.Level, _enemySpawner.GetRandomSpawnPoint(), out EnemyPresenter newEnemy))
        {
            newEnemy.gameObject.SetActive(true);
        }
    }
}
