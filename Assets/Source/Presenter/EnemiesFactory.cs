using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private List<EnemyPresenter> _enemies;

    private Transform _defaultParent;
    private Dictionary<EnemyPresenter, Range> _enemieLevelsRange;

    public void Initialize(Transform defaultParent)
    {
        _defaultParent = defaultParent;

        _enemieLevelsRange = new Dictionary<EnemyPresenter, Range>();
        foreach (var enemy in _enemies)
        {
            _enemieLevelsRange.Add(enemy, enemy.GetMinMaxLevel());
        }
    }

    public EnemyPresenter CreateRandomEnemy(SimpleEnemy model, int level, Vector2 position)
    {
        var collection = from enemy in _enemieLevelsRange
                         where enemy.Value.Start.Value <= level && enemy.Value.End.Value >= level
                         select enemy.Key;

        var avaliableEnemies = collection.ToArray();

        EnemyPresenter enemyPresenterTemplate = null;

        if (avaliableEnemies.Length == 0)
            return null;

        int randomIndex = UnityEngine.Random.Range(0, avaliableEnemies.Length);
        enemyPresenterTemplate = avaliableEnemies[randomIndex];

        EnemyPresenter enemyPresenter = Instantiate(enemyPresenterTemplate, position, Quaternion.identity, _defaultParent);
        enemyPresenter.Initialize(model, level);
        enemyPresenter.EndInitialize();

        return enemyPresenter;
    }
}
