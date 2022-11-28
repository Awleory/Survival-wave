using System;
using System.Collections.Generic;

public class Stats : IStartable
{
    public event Action LevelUp;
    public event Action Updated;

    public Attribute Health { get; private set; } = new Attribute(nameof(Health));
    public Attribute Attack { get; private set; } = new Attribute(nameof(Attack));
    public Attribute Defence { get; private set; } = new Attribute(nameof(Defence));
    public Attribute MoveSpeed { get; private set; } = new Attribute(nameof(MoveSpeed));
    public Attribute ExpRequired { get; private set; } = new Attribute(nameof(ExpRequired));
    public Attribute AttackSpeed { get; private set; } = new Attribute(nameof(AttackSpeed));

    public float DamageResist => GetResist();
    public float AttacksPerSecond => CalculateHyperboleFunc(AttackSpeed.Value, 10, 0.05f, 5);

    public int Level { get; private set; } = 1;
    public int MaxLevel { get; private set; }
    public int MinLevel { get; private set; }
    public int CurrentExp { get; private set; }

    private List<Attribute> _statAttributes;

    public Stats()
    {
        _statAttributes = new List<Attribute>{Health, Attack, Defence, MoveSpeed, AttackSpeed, ExpRequired};
    }

    public void Initialize(CharacterStatsConfig statsConfig, int level)
    {
        Level = Math.Max(Config.MinCharacterLevel, level);
        MinLevel = statsConfig.MinLevel;
        MaxLevel = statsConfig.MaxLevel;

        Health.Initialize(statsConfig.BaseHealth, statsConfig.MaxHealth, Level, statsConfig.MinLevel, statsConfig.MaxLevel);
        Attack.Initialize(statsConfig.BaseAttack, statsConfig.MaxAttack, Level, statsConfig.MinLevel, statsConfig.MaxLevel);
        Defence.Initialize(statsConfig.BaseDefence, statsConfig.MaxDefence, Level, statsConfig.MinLevel, statsConfig.MaxLevel);
        MoveSpeed.Initialize(statsConfig.BaseRunSpeed, statsConfig.MaxRunSpeed, Level, statsConfig.MinLevel, statsConfig.MaxLevel);
        AttackSpeed.Initialize(statsConfig.BaseAttackSpeed, statsConfig.MaxAttackSpeed, Level, statsConfig.MinLevel, statsConfig.MaxLevel);
        ExpRequired.Initialize(Config.BaseExpNeed, Config.MaxExpNeed, Level, statsConfig.MinLevel, statsConfig.MaxLevel);
    }

    public void Start()
    {
        Update();
    }

    public void TakeExp(int expPoints)
    {
        if (Level >= MaxLevel)
            return;

        if (expPoints <= 0)
            throw new ArgumentOutOfRangeException(nameof(expPoints));

        while (expPoints > 0)
        {
            int needToLevelUp = (int)ExpRequired.Value - CurrentExp;
            if (needToLevelUp == 0)
                return;

            if (expPoints >= needToLevelUp)
            {
                UpLevel();
                expPoints -= needToLevelUp;
            }
            else
            {
                CurrentExp += expPoints;
                expPoints = 0;
            }
        }
    }

    public void UpLevel()
    {
        if (LevelIsCorrect(Level + 1) == false)
            return;

        CurrentExp = 0;
        Level++;

        Update();

        LevelUp?.Invoke();
    }

    public void SetLevel(int level)
    {
        if (LevelIsCorrect(level) == false)
            return;

        Level = level;
        CurrentExp = 0;
        Update();
    }

    public string GetStatsInfo()
    {
        string statsInfo = "";
        foreach (var attribute in _statAttributes)
        {
            statsInfo += attribute.Label + " - " + attribute.Value + "\n";
        }
        return statsInfo;
    }

    private bool LevelIsCorrect(int level)
    {
        if (level > MaxLevel || level < MinLevel)
            return false;

        return true;
    }

    private float GetResist()
    {
        return CalculateHyperboleFunc(Defence.Value, 10, 0.06f, 1) - 1;
    }

    private float CalculateHyperboleFunc(float x, float growthRate, float xRate, float limit)
    {
        return limit * (xRate * x / (growthRate + xRate * x)) + 1;
    }

    private void Update()
    {
        foreach (var attribute in _statAttributes)
        {
            attribute.Update(Level);
        }

        if (Level == MaxLevel)
            CurrentExp = (int)ExpRequired.Value;

        Updated?.Invoke();
    }
}
