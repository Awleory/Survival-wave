using UnityEngine;

[RequireComponent(typeof(EnemyTag))]
[RequireComponent(typeof(Rewards))]
public class EnemyPresenter : CharacterPresenter<SimpleEnemy>
{
    public Rewards Rewards { get; private set; }
    public EnemyTag EnemyTag { get; private set; }

    public override void Initialize(SimpleEnemy model)
    {
        base.Initialize(model);

        EnemyTag.Initialize(model);
    }

    protected override void Awake()
    {
        base.Awake();

        Rewards = GetComponent<Rewards>();
        EnemyTag = GetComponent<EnemyTag>();
    }

    protected override void OnDied()
    {
        int rewardExp = (int)(Rewards.BaseExperience + Rewards.BaseExperience * Rewards.ExperienceRatePerLevel * Model.Stats.Level);
        Model.Target.TakeExp(rewardExp);

        base.OnDied();
    }
}
