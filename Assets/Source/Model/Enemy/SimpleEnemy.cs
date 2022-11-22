using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    public event Action Attacked;

    private TimerActions _timerActions;
    private int _actionAttackID;
    private bool _attackIsReady = false;

    private const float _minDistanceToTarget = 0.4f;
    private const bool _dealPureDamage = false;

    public SimpleEnemy(Player target) : base(target) 
    {
        _timerActions = new TimerActions();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        _timerActions.GotReady += OnTimerGotReady;
        _actionAttackID = _timerActions.Create(AttacksPerSecond);

        Stats.AttackSpeed.ValueChanged += OnAttackSpeedChanged;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        _timerActions.GotReady -= OnTimerGotReady;

        Stats.AttackSpeed.ValueChanged -= OnAttackSpeedChanged;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _timerActions.Update(deltaTime);

        if (IsAlive)
        {
            if (DistanceToTarget <= _minDistanceToTarget)
            {
                Movement.Move(Vector2.zero);
                TryAttack(Target);
            }
            else
            {
                Movement.Move((Target.Movement.Position - Movement.Position).normalized);
            }
        }
    }

    private void OnTimerGotReady(int id)
    {
        if (id == _actionAttackID)
            _attackIsReady = true;
    }

    private void TryAttack(Character target)
    {
        if (_attackIsReady)
        {
            target.ApplyDamage(Damage, _dealPureDamage);
            Attacked?.Invoke();
            _attackIsReady = false;
        }
    }

    private void OnAttackSpeedChanged()
    {
        _timerActions.SetCooldown(_actionAttackID, AttacksPerSecond);
    }
}
