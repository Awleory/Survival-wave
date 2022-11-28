using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : Character
{
    public event Action<Weapon> ChangedWeapon;

    public Weapon CurrentWeapon { get; private set; }

    private List<Weapon> _weapons = new List<Weapon>();
    private PlayerController _controller;

    public Player()
    {
        _controller = new PlayerController();
    }

    public void Initialize()
    {
        CurrentWeapon = new Weapon();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        _controller.Shot += OnShoot;
        _controller.ChoseNextWeapon += OnChoseNextWeapon;
        _controller.ChosePreviousWeapon += OnChosePreviousWeapon;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        _controller.Shot -= OnShoot;
        _controller.ChoseNextWeapon -= OnChoseNextWeapon;
        _controller.ChosePreviousWeapon -= OnChosePreviousWeapon;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _controller.Update(deltaTime);
        CurrentWeapon.Update(deltaTime);

        Movement.Move(_controller.Velocity);

        CurrentWeapon.OnMouseMoved(_controller.ScreenMousePosition);
    }

    public void GiveWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);

        if (CurrentWeapon == null)
            CurrentWeapon = weapon;
    }

    public override void Respawn(Vector2 spawnPoint, bool restoreHealth = true, int level = 0)
    {
        base.Respawn(spawnPoint, restoreHealth, level);
        _controller.UnFreeze();
    }

    private void OnShoot()
    {
        if (IsAlive)
            CurrentWeapon.TryShoot();
    }

    private void ChangeWeapon(Weapon weapon)
    {
        if (CurrentWeapon == weapon)
            return;

        CurrentWeapon = weapon;

        ChangedWeapon?.Invoke(CurrentWeapon);
    }

    private void OnChoseNextWeapon()
    {
        if (_weapons.Count != 0)
        {
            int currentIndex = _weapons.IndexOf(CurrentWeapon);
            ChangeWeapon(_weapons[(int)Mathf.Repeat(currentIndex + 1, _weapons.Count)]);
        }
    }

    private void OnChosePreviousWeapon()
    {
        if (_weapons.Count != 0)
        {
            int currentIndex = _weapons.IndexOf(CurrentWeapon);
            ChangeWeapon(_weapons[(int)Mathf.Repeat(currentIndex - 1, _weapons.Count)]);
        }
    }

    protected override void OnDied()
    {
        _controller.Freeze();
        base.OnDied();
    }
}

