using UnityEngine;

public class Player : Character
{
    public Weapon Weapon { get; private set; }

    public Vector2 MousePosition => _mousePositionScale * ((Vector2)Camera.main.ScreenToViewportPoint(_controller.MousePosition) + _mouseOffset);

    private PlayerController _controller;

    private Vector2 _mouseOffset = new Vector2(-0.5f, -0.5f);
    private const float _mousePositionScale = 2;

    public Player()
    {
        Weapon = new Weapon();
        _controller = new PlayerController();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        _controller.Shot += OnShoot;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        _controller.Shot -= OnShoot;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _controller.Update(deltaTime);
        Weapon.Update(deltaTime);

        Movement.Move(_controller.Velocity);

        Weapon.OnMouseMoved(MousePosition);
    }

    private void OnShoot()
    {
        Weapon.TryShoot();
    }
}

