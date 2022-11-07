using UnityEngine;

public class PlayerController : IUpdateble
{
    private PlayerInput _input;
    private Movement _movement;

    public PlayerController(Movement movement)
    {
        _movement = movement;

        _input = new PlayerInput();
        _input.Enable();
    }

    public void Update(float deltatime)
    {
        Vector2 velocity = _input.Player.Move.ReadValue<Vector2>();
        _movement.Move(velocity);
    }
}
