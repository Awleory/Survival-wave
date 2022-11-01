using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;

    private Player _player;

    private void Awake()
    {
        _player = new Player();
        _playerPresenter.Initialize(_player, new CharacterHealthPolicy());
    }
}
