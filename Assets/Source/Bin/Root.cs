using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private EnemyPresentersFactory _enemyPresentersFactory;
    [SerializeField] private EnemySpawnerPresenter _enemySpawner;
    [SerializeField] private Ground _background;

    private Player _player;

    private void Awake()
    {
        _player = new Player();

        _playerPresenter.Initialize(_player, 0);
        _playerPresenter.EndInitialize();

        _background.Initialize(_player.Movement);
        
        _enemySpawner.Initialize(_player);
        _enemySpawner.EndInitialize();
    }
}
