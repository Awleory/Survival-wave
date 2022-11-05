using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Color _maxValue;
    [SerializeField] private Color _highValue;
    [SerializeField] private Color _lowValue;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textValue;

    private Health _health;
    private string _rawTextValue = "{0}%";
    private float _maxTextValue = 100;
    private int _minSliderValue = 0;
    private int _maxSliderValue = 1;
    private Coroutine _healthChangeCoroutine;
    private float _normalizedHealth
    {
        get
        {
            if (_health.MaxValue == 0)
                return 0;
            else
                return (float)Math.Round((double)(_health.Value / _health.MaxValue), 2);
        }
    }

    private void OnEnable()
    {
        _health.ValueChanged += OnUpdateSlider;

        _slider.minValue = _minSliderValue;
        _slider.maxValue = _maxSliderValue;
        _slider.value = _normalizedHealth;

        _textValue.text = string.Format(_rawTextValue, _slider.value * _maxTextValue);
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnUpdateSlider;
    }

    public void Initialize(Health health)
    {
        _health = health;

        enabled = true;
    }

    private void OnUpdateSlider(float delta)
    {
        if (_healthChangeCoroutine != null)
        {
            StopCoroutine(_healthChangeCoroutine);
        }
        _healthChangeCoroutine = StartCoroutine(ChangeSliderCoroutine(_normalizedHealth));
    }

    private IEnumerator ChangeSliderCoroutine(float targetValue)
    {
        int decimalPlaces = 2;

        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, Time.deltaTime);

            float roundedTargetValue = (float)Math.Round((double)(_slider.value), decimalPlaces);
            _textValue.text = string.Format(_rawTextValue, roundedTargetValue * _maxTextValue);
            yield return null;
        }
        _healthChangeCoroutine = null;
    }
}
