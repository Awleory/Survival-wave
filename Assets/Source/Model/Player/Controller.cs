using UnityEngine;

public class Controller : IUpdateble
{
    private PlayerInput _input;
    private MovementPhysics _movementPhysics;
    private Vector2 _velocity;
    private Entity _entity;

    public Controller(Entity entity)
    {
        _entity = entity;

        _input = new PlayerInput();
        _input.Enable();
    }

    public void FixedUpdate(float deltaTime) { }

    public void Update(float deltatime)
    {
        _velocity = _input.Player.Move.ReadValue<Vector2>();
        _entity.MovementPhysics.Move(_velocity);
    }
}
