using UnityEngine;

public class Player : Character
{
    public Weapon Weapon { get; private set; }

    private PlayerController _controller;

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

        Weapon.OnMouseMoved(_controller.ScreenMousePosition);
    }

    private void OnShoot()
    {
        Weapon.TryShoot();
    }
}

