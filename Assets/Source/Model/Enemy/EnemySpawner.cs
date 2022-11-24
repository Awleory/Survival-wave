using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : IEnable, IUpdateble
{
    public Player Target { get; private set; }

    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    private List<Vector2> _spawnPoints; 
    private float _spawnLineRadius;
    private int _spawnPointsCount;

    private const float _minRespawnDistanceToTarget = 15f;
    private const float _spawnSpread = 2;

    public void Initialize(Player target, float spawnRadius, int spawnPointsCount)
    {
        Target = target;
        _spawnLineRadius = spawnRadius;
        _spawnPointsCount = spawnPointsCount;

        _spawnPoints = CreateSpawnPoints();
    }

    public void OnEnable() 
    {
        foreach (Enemy enemy in _spawnedEnemies)
            ProcessListeners(enemy, true);
    }

    public void OnDisable()
    {
        foreach (Enemy enemy in _spawnedEnemies)
            ProcessListeners(enemy, false);
    }

    public SimpleEnemy CreateSimpleEnemy()
    {
        var newEnemy = new SimpleEnemy(Target);
        ProcessListeners(newEnemy, true);
        _spawnedEnemies.Add(newEnemy);

        return newEnemy;
    }

    public Vector3 GetRandomSpawnPoint(bool affectSpread = true)
    {
        if (_spawnPoints.Count == 0)
            return Vector3.zero;

        int spawnPointIndex = Random.Range(0, _spawnPoints.Count);
        Vector2 spawnPoint = _spawnPoints[spawnPointIndex];
        
        Vector2 targetPosition = Target.Movement.Position;

        float xSpread = 0;
        float ySpread = 0;
        if (affectSpread)
        {
            xSpread = Random.Range(-_spawnSpread, _spawnSpread);
            ySpread = Random.Range(-_spawnSpread, _spawnSpread);
        }

        return new Vector2 (spawnPoint.x + targetPosition.x + xSpread, spawnPoint.y + targetPosition.y + ySpread);
    }

    public void Update(float deltaTime)
    {
        foreach (Enemy enemy in _spawnedEnemies)
        {
            if (enemy.DistanceToTarget >= _minRespawnDistanceToTarget)
                enemy.Respawn(GetRandomSpawnPoint(), true);
        }
    }

    private List<Vector2> CreateSpawnPoints()
    {
        float degrees = 360;
        float angleStep = degrees / _spawnPointsCount;

        List<Vector2> spawnPoints = new List<Vector2>();

        for (int i = 0; i < _spawnPointsCount; i++)
        {
            float x = _spawnLineRadius * Mathf.Cos(angleStep * (i + 1) * Mathf.Deg2Rad);
            float y = _spawnLineRadius * Mathf.Sin(angleStep * (i + 1) * Mathf.Deg2Rad);
            Vector2 spawnPoint = new Vector2(x, y);
            spawnPoints.Add(spawnPoint);
        }

        return spawnPoints;
    }

    private void OnEnemyDestryed(Character character)
    {
        if (_spawnedEnemies.Contains((Enemy)character))
            _spawnedEnemies.Remove((Enemy)character);
    }

    private void ProcessListeners(Enemy enemy, bool add)
    {
        if (add)
        {
            enemy.Destroyed += OnEnemyDestryed;
        }
        else
        {
            enemy.Destroyed -= OnEnemyDestryed;
        }
    }
}
