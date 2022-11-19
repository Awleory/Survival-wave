using TMPro.EditorUtilities;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private EnemyPresentersFactory _enemyPresentersFactory;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Ground _background;

    private Player _player;

    private void Awake()
    {
        _player = new Player();

        _playerPresenter.Initialize(_player);
        _playerPresenter.EndInitialize();

        _background.Initialize(_player.Movement);
        
        _enemySpawner.Initialize(_player);
    }
}
