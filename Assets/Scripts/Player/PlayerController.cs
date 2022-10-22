using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerInput _input;
    private Player _player;
    private Vector2 _velocity;

    public PlayerController(Player player)
    {
        _player = player;
        _input = new PlayerInput();

        _input.Enable();
    }

    public void UpdateMove()
    {
        _velocity = _input.Player.Move.ReadValue<Vector2>();
        _player.MovementPhysics.Move(_velocity);
    }
}
