using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesFactory : ObjectPool<EnemyPresenter>
{
    [SerializeField] private List<EnemyPresenter> _enemies;

    private Dictionary<EnemyPresenter, Range> _enemieLevelsRange;
    private EnemySpawner _enemySpawner;

    public void Initialize(int maxEnemies, EnemySpawner enemySpawner, Transform container)
    {
        _enemySpawner = enemySpawner;
        int poolCapacity = maxEnemies / _enemies.Count;

        _enemieLevelsRange = new Dictionary<EnemyPresenter, Range>();
        foreach (EnemyPresenter enemyTemplate in _enemies)
        {
            _enemieLevelsRange.Add(enemyTemplate, enemyTemplate.GetMinMaxLevel());
            Initialize(enemyTemplate, poolCapacity, container);
        }
    }

    public bool TryGetRandomEnemy(int level, Vector2 position, out EnemyPresenter result)
    {
        result = null;

        var collection = from enemy in _enemieLevelsRange
                         where enemy.Value.Start.Value <= level && enemy.Value.End.Value >= level
                         select enemy.Key;

        var avaliableEnemies = collection.ToArray();
        if (avaliableEnemies.Length == 0)
            return false;

        EnemyPresenter enemyPresenterTemplate = null;
        int randomIndex = UnityEngine.Random.Range(0, avaliableEnemies.Length);
        enemyPresenterTemplate = avaliableEnemies[randomIndex];

        EnemyPresenter enemyPresenter;
        if (TryGetObject(enemyPresenterTemplate, out enemyPresenter, true))
        {
            if (enemyPresenter.Model == null)
            {
                enemyPresenter.Initialize(_enemySpawner.CreateSimpleEnemy(), level);
                enemyPresenter.EndInitialize();
            }
   
            enemyPresenter.Respawn(position, true, level);
            result = enemyPresenter;
        }

        return result != null;
    }
}
