using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private bool _hideWhenFull = false;
    [SerializeField] private bool _showText = true;
    [SerializeField] private float _fillSpeedRate = 1; 
    [SerializeField] private TextMeshProUGUI _textValue;
    [SerializeField] private Image _fillArea;

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

    private Health _health;
    private string _rawTextValue = "{0}/{1}";
    private Coroutine _healthChangeCoroutine;

    private void OnEnable()
    {
        _health.ValueChanged += OnUpdateSlider;

        _fillArea.fillAmount = _normalizedHealth;

        UpdateHealthText();
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnUpdateSlider;
    }

    public void Initialize(Health health)
    {
        _health = health;

        enabled = true;
        OnUpdateSlider();
    }

    private void OnUpdateSlider(float delta = 0)
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

        if (_fillArea.fillAmount == targetValue)
        {
            UpdateHealthText();
        }

        while (_fillArea.fillAmount != targetValue)
        {
            _fillArea.fillAmount = Mathf.MoveTowards(_fillArea.fillAmount, targetValue, Time.deltaTime * _fillSpeedRate);
            float roundedCurrentValue = (float)Math.Round((double)(_fillArea.fillAmount * _health.MaxValue), decimalPlaces);
            UpdateHealthText();
            yield return null;
        }
        _healthChangeCoroutine = null;
    }

    private void UpdateHealthText()
    {
        if (_showText)
            _textValue.text = string.Format(_rawTextValue, (int)_health.Value, _health.MaxValue);
        else if (_textValue.gameObject.activeInHierarchy)
            _textValue.gameObject.SetActive(false);

        if (_hideWhenFull)
            gameObject.SetActive(_health.Value != _health.MaxValue);
    }
}
