using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;

    private Player<CharacterHealth> _player;

    private void Awake()
    {
        _player = new Player<CharacterHealth>(new CharacterHealth());
        _playerPresenter.Initialize(_player);
    }
}
