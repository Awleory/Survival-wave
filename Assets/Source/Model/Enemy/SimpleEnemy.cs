using UnityEngine;

public class SimpleEnemy : Enemy
{
    private bool _minDistanceToTargetReached => Vector2.Distance(Target.Movement.Position, Movement.Position) <= _minDistanceToTarget;
    private TimerActions _timerActions;
    private int _actionAttackID;
    private bool _attackIsReady = false;

    private const float _minDistanceToTarget = 0.4f;

    public SimpleEnemy(Player target) : base(target) 
    {
        _timerActions = new TimerActions();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _timerActions.GotReady += OnTimerGotReady;

        _actionAttackID = _timerActions.Create(AttacksPerSecond);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _timerActions.GotReady -= OnTimerGotReady;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        _timerActions.Update(deltaTime);

        if (_minDistanceToTargetReached)
        {
            Movement.Move(Vector2.zero);
            if (_attackIsReady)
            {
                Target.ApplyDamage(Damage, DamageType.Physical);
                _attackIsReady = false;
            }
        }
        else
        {
            Movement.Move((Target.Movement.Position - Movement.Position).normalized);
        }   
    }

    private void OnTimerGotReady(int id)
    {
        if (id == _actionAttackID)
            _attackIsReady = true;
    }

    protected override void OnAttributeBonusesUpdated()
    {
        base.OnAttributeBonusesUpdated();

        _timerActions.SetCooldown(_actionAttackID, AttacksPerSecond);
    }
}
