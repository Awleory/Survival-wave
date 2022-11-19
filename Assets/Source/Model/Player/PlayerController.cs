using System;
using UnityEngine;

public class PlayerController : IUpdateble
{
    public event Action Shot;

    public Vector2 ScreenMousePosition { get; private set; }
    public Vector2 Velocity { get; private set; }

    private PlayerInput _input;
    private bool _shootButtonPressed = false;

    private const float valueWhenShooting = 1;

    public PlayerController()
    {
        _input = new PlayerInput();
        _input.Enable();
    }

    public void Update(float deltatime)
    {
        Velocity = _input.Player.Move.ReadValue<Vector2>();

        _shootButtonPressed = _input.Player.Shoot.ReadValue<float>() == valueWhenShooting;
       
        if (_shootButtonPressed)
            Shot?.Invoke();

        ScreenMousePosition = _input.Player.MousePosition.ReadValue<Vector2>();
    }
}
