using System;

public class Attribute 
{
    public event Action ValueChanged;
    public string Label { get; private set; }
    public float Value { get; private set; }

    private float _baseValue;
    private float _maxValue;
    private float _ratio;
    private int _minLevel;
    private int _maxLevel;

    public Attribute(string label)
    {
        Label = label;
    }

    public void Initialize(float baseValue, float maxValue, int level, int minLevel, int maxLevel)
    {
        _baseValue = baseValue;
        _maxValue = maxValue;
        Value = baseValue;
        _minLevel = minLevel;
        _maxLevel = maxLevel;

        _ratio = (_maxValue - _baseValue) / GetSumOfIntsInRow(_minLevel, _maxLevel);

        Update(level);
    }

    public void Update(int level)
    {
        if (level < 0)
            throw new ArgumentOutOfRangeException(nameof(level));

        Value = _ratio * (GetSumOfIntsInRow(_minLevel, level)) + _baseValue;

        ValueChanged?.Invoke();
    }

    private int GetSumOfIntsInRow(int firstNumber, int secondNumber)
    {
        int half = 2;

        int resultSum = secondNumber * (secondNumber + 1) / half;

        if (firstNumber != 1)
        {
            resultSum -= firstNumber * (firstNumber - 1) / half;
        }

        return resultSum;
    }
}
