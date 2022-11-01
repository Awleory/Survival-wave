using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStats
{
    public event Action LevelUp;

    public StatAttribute Vitality;
    public StatAttribute Strength;
    public StatAttribute Intellect;

    private const float _expForNextLevelRate = 1.2f;

    public int Level { get; private set; } = 1;
    public int CurrentExp { get; private set; }
    public int ExpForNextLevel { get; private set; } = 100;

    public void Initialize(CharacterStatsConfig statsConfig, int level)
    {
        Vitality = statsConfig.Vitality;
        Strength = statsConfig.Strength;
        Intellect = statsConfig.Intellect;

        Level = level;
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
    }
}
