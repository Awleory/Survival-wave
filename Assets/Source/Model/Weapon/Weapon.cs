using System;
using UnityEngine;

public class Weapon : IUpdateble, IEnable
{
    public event Action Shot;
    public event Action<Vector2> Rotated;

    private float _shootsPerSecond => 1 / _shootingSpeed;
    private float _shootingSpeed;
    private bool _readyForShoot;
    private TimerActions _timerActions;
    private int _shootActionID;

    public Weapon()
    {
        _timerActions = new TimerActions();
    }

    public void Initialize(float shootingSpeed, Vector2 offset)
    {
        _shootingSpeed = shootingSpeed;
        _shootActionID = _timerActions.Create(_shootsPerSecond);
    }

    public void Update(float deltaTime)
    {
        _timerActions.Update(deltaTime);
    }

    public void OnEnable()
    {
        _timerActions.GotReady += OnTimerGotReady;
    }

    public void OnDisable()
    {
        _timerActions.GotReady -= OnTimerGotReady;
    }

    public void TryShoot()
    {
        if (_readyForShoot)
        {
            Shot?.Invoke();
            _readyForShoot = false;
        }
    }

    public void OnMouseMoved(Vector2 screenMousePosition)
    {
        Rotated?.Invoke(screenMousePosition);
    }

    private void OnTimerGotReady(int actionID)
    {
        if (actionID == _shootActionID)
            OnReadyForShoot();
    }

    private void OnReadyForShoot()
    {
        _readyForShoot = true;
    }
}
