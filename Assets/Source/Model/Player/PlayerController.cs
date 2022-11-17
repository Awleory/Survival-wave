using System;
using UnityEngine;

public class PlayerController : IUpdateble
{
    public event Action Shot;

    public Vector2 ScreenMousePosition { get; private set; }
    public Vector2 Velocity { get; private set; }

    private PlayerInput _input;

    public PlayerController()
    {
        _input = new PlayerInput();
        _input.Enable();
        
        _input.Player.Shoot.performed += context => OnShoot();
    }

    public void Update(float deltatime)
    {
        Velocity = _input.Player.Move.ReadValue<Vector2>();
        ScreenMousePosition = _input.Player.MousePosition.ReadValue<Vector2>();
    }

    private void OnShoot()
    {
        Shot?.Invoke();
    }
}
