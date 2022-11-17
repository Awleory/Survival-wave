using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private Transform _spawnedEnemies;
    [SerializeField] private Transform _points;
    [SerializeField, Min(0)] private int _maxEnemies = 1;
    [SerializeField, Min(0)] private float _delaySpawn;
    [SerializeField, Min(0)] private float _spawnLineRadius;
    [SerializeField, Min(1)] private int _spawnPointsCount;
    [SerializeField] private bool _enable;

    private Player _target;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _delay;

    public void Initialize(Player target)
    {
        _target = target;
        _factory.Initialize(_spawnedEnemies);

        CreateSpawnPoints();

        enabled = _enable;
    }

    private void Awake()
    {
        _delay = new WaitForSeconds(_delaySpawn);
    }

    private void Update()
    {
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(SpawnCoroutine(_delay));
    }

    private IEnumerator SpawnCoroutine(WaitForSeconds delay)
    {
        yield return delay;
        while(_spawnedEnemies.childCount < _maxEnemies)
        {
            Spawn();
            yield return delay;
        }
        _spawnCoroutine = null;
    }

    private void Spawn()
    {
        int spawnPointIndex = Random.Range(0, _points.childCount - 1);
        _factory.CreateBat(new SimpleEnemy(_target), _points.GetChild(spawnPointIndex).transform.position);
    }

    private void CreateSpawnPoints()
    {
        float degrees = 360;
        float angleStep = degrees / _spawnPointsCount;

        for (int i = 0; i < _spawnPointsCount; i++)
        {
            GameObject pointGameObject = new GameObject();
            pointGameObject.name = "point " + i;

            Transform pointTransform = pointGameObject.GetComponent<Transform>();
            pointTransform.parent = _points; 

            float x = _spawnLineRadius * Mathf.Cos(angleStep * (i + 1) * Mathf.Deg2Rad);
            float y = _spawnLineRadius * Mathf.Sin(angleStep * (i + 1) * Mathf.Deg2Rad);
            pointTransform.position = new Vector3(x, y);
        }
    }
}
