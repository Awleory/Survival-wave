using UnityEngine;

public class Controller : IUpdateble
{
    private PlayerInput _input;
    private Movement _movement;

    public Controller(Movement movement)
    {
        _movement = movement;

        _input = new PlayerInput();
        _input.Enable();
    }

    public void Update(float deltatime)
    {
        Vector2 velocity = _input.Player.Move.ReadValue<Vector2>();
        if (velocity != Vector2.zero)
            _movement.Move(velocity);
    }
}
