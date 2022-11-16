using System;
using UnityEngine;

public class Weapon : IUpdateble, IEnable
{
    public event Action Shot;
    public event Action Rotated;

    public Vector2 Direction { get; private set; }

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

    public void OnMouseMoved(Vector2 mousePosition)
    {
        mousePosition += new Vector2(0f, 0.05f);
        Direction = mousePosition.normalized;
        Rotated?.Invoke();
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
