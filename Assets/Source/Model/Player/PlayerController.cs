using System;
using UnityEngine;

public class PlayerController : IUpdateble
{
    public event Action Shot;
    public event Action ChoseNextWeapon;
    public event Action ChosePreviousWeapon;

    public Vector2 ScreenMousePosition { get; private set; }
    public Vector2 Velocity { get; private set; }

    private PlayerInput _input;
    private bool _shootButtonPressed = false;
    private bool _frozen = false;

    private const float valueWhenShooting = 1;

    public PlayerController()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.NextWeapon.performed += context => OnNextWeapon();
        _input.Player.PreviousWeapon.performed += context => OnPreviousWeapon();
    }

    public void Update(float deltatime)
    {
        if (_frozen)
            return;

        Velocity = _input.Player.Move.ReadValue<Vector2>();

        _shootButtonPressed = _input.Player.Shoot.ReadValue<float>() == valueWhenShooting;
       
        if (_shootButtonPressed)
            Shot?.Invoke();

        ScreenMousePosition = _input.Player.MousePosition.ReadValue<Vector2>();
    }

    public void Freeze()
    {
        _frozen = true;
    }

    public void UnFreeze()
    {
        _frozen = false;
    }

    private void OnNextWeapon()
    {
        ChoseNextWeapon?.Invoke();
    }

    private void OnPreviousWeapon()
    {
        ChosePreviousWeapon?.Invoke();
    }
}
