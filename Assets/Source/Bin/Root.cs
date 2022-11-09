using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private EnemyPresentersFactory _enemyPresentersFactory;

    private Player _player;

    private void Awake()
    {
        _player = new Player();

        _playerPresenter.Initialize(_player);

        _enemyPresentersFactory.CreateSimpleEnemy(new SimpleEnemy(_player));
    }
}
