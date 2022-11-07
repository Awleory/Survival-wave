using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;

    public Player Player { get; private set; }

    private void Awake()
    {
        Player = new Player();

        _playerPresenter.Initialize(Player);
    }
}
