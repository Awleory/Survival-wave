using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private EnemySpawnerPresenter _enemySpawner;
    [SerializeField] private Ground _background;
    [SerializeField] private EndScreen _endScreen;

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

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _endScreen.RestartButtonPressed += OnRestartButtonPressed;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _endScreen.RestartButtonPressed -= OnRestartButtonPressed;
    }

    public void RestartGame()
    {
        _playerPresenter.Respawn(Vector2.zero, true, Config.MinCharacterLevel);
        _enemySpawner.Respawn();
    }

    private void OnRestartButtonPressed()
    {
        _endScreen.Close();
        RestartGame();
    }

    private void OnPlayerDied()
    {
        _endScreen.Open();
    }
}
