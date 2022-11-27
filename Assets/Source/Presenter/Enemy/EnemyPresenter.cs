using UnityEngine;

[RequireComponent(typeof(EnemyTag))]
[RequireComponent(typeof(Rewards))]
public class EnemyPresenter : CharacterPresenter<SimpleEnemy>
{
    public Rewards Rewards { get; private set; }
    public EnemyTag EnemyTag { get; private set; }

    public override void Initialize(SimpleEnemy model, int level)
    {
        Rewards = GetComponent<Rewards>();
        EnemyTag = GetComponent<EnemyTag>();

        base.Initialize(model, level);

        EnemyTag.Initialize(model);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.Attacked += OnAttacked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.Attacked -= OnAttacked;
    }

    protected override void OnDying()
    {
        int rewardExp = (int)(Rewards.BaseExperience + Rewards.BaseExperience * Rewards.ExperienceRatePerLevel * Model.Stats.Level);
        Model.Target.TakeExp(rewardExp);

        base.OnDying();
    }

    private void OnAttacked()
    {
        AnimationController.OnAttacked();
    }
}
