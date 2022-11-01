using System;

public class StatAttribute 
{
    public event Action ValueChanged;
    public float Value { get; private set; }

    private float _baseValue;
    private float _deltaPerLevel;

    public StatAttribute(float baseValue, float deltaPerLevel, int level)
    {
        _baseValue = baseValue;
        _deltaPerLevel = deltaPerLevel;

        Update(level);
    }

    public void Update(int level)
    {
        if (level < 0)
            throw new ArgumentOutOfRangeException(nameof(level));

        Value = _baseValue + _deltaPerLevel * level;
    }
}
