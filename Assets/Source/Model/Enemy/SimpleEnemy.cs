using UnityEngine;

public class SimpleEnemy : Enemy
{
    private const float _minDistanceToTarget = 0.2f;

    public SimpleEnemy(Player target) : base(target) 
    {
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        float distance = Vector2.Distance(Target.Movement.Position, Movement.Position);
        
        if (distance >= _minDistanceToTarget)
        {
            Movement.Move((Target.Movement.Position - Movement.Position).normalized);
        }
        else
        {
            Movement.Move(Vector2.zero);
        }   
    }
}
