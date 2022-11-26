using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : IEnable
{
    public Player Target { get; private set; }

    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    private List<Vector2> _spawnPoints; 
    private float _spawnLineRadius;
    private int _spawnPointsCount;

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
        {
            UpdateListeners(enemy, true);
        }
    }

    public void OnDisable()
    {
        foreach (Enemy enemy in _spawnedEnemies)
        {
            UpdateListeners(enemy, false);
        }
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

    public SimpleEnemy CreateSimpleEnemy()
    {
        SimpleEnemy newEnemy = new SimpleEnemy(Target);
        _spawnedEnemies.Add(newEnemy);
        UpdateListeners(newEnemy, true);

        return newEnemy;
    }

    private List<Vector2> CreateSpawnPoints()
    {
        float circleDegrees = 360;
        float angleStep = circleDegrees / _spawnPointsCount;

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

    private void OnGotRespawnDistance(Enemy enemy)
    {
        enemy.Respawn(GetRandomSpawnPoint());
    }

    private void UpdateListeners(Enemy enemy, bool add)
    {
        if (add)
        {
            enemy.GotRespawnDistance += OnGotRespawnDistance;
        }
        else
        {
            enemy.GotRespawnDistance -= OnGotRespawnDistance;
        }
    }
}
