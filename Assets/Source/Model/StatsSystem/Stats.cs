using System;

public class Stats : IStartable
{
    public event Action LevelUp;
    public event Action Updated;

    public StatAttribute Vitality { get; private set; } = new StatAttribute();
    public StatAttribute Strength { get; private set; } = new StatAttribute();
    public StatAttribute Dexterity { get; private set; } = new StatAttribute();
    public StatAttribute Intellect { get; private set; } = new StatAttribute();

    private const float _expForNextLevelRate = 1.2f;

    public int Level { get; private set; } = 1;
    public int CurrentExp { get; private set; }
    public int ExpForNextLevel { get; private set; } = 100;

    public void Initialize(CharacterStatsConfig statsConfig, int level)
    {
        Vitality.Initialize(statsConfig.BaseVitality, statsConfig.DeltaVitality, level);
        Strength.Initialize(statsConfig.BaseStrength, statsConfig.DeltaStrength, level);
        Intellect.Initialize(statsConfig.BaseIntellect, statsConfig.DeltaIntellect, level);
        Dexterity.Initialize(statsConfig.BaseDexterity, statsConfig.DeltaDexterity, level);

        Level = level;
    }

    public void Start()
    {
        Update();
    }

    public void TakeExp(int expPoints)
    {
        if (expPoints <= 0)
            throw new ArgumentOutOfRangeException(nameof(expPoints));

        while (expPoints > 0)
        {
            int needToLevelUp = ExpForNextLevel - CurrentExp;

            if (expPoints >= needToLevelUp)
            {
                UpLevel();
                expPoints -= needToLevelUp;
            }
            else
            {
                CurrentExp = expPoints;
            }
        }
    }

    public void UpLevel()
    {
        Level++;
        ExpForNextLevel = (int)(ExpForNextLevel * _expForNextLevelRate);

        Update();
        LevelUp?.Invoke();
    }

    private void Update()
    {
        Vitality.Update(Level);
        Strength.Update(Level);
        Intellect.Update(Level);

        Updated?.Invoke();
    }
}
