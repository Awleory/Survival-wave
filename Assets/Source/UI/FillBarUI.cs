using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillBarUI : MonoBehaviour
{
    [SerializeField] private bool _hideWhenFull = false;
    [SerializeField] private bool _showText = true;
    [SerializeField] private float _fillSpeedRate = 1; 
    [SerializeField] private TextMeshProUGUI _textValue;
    [SerializeField] private Image _foreground;
    [SerializeField] private Image _background;

    private float _value;
    private float _maxValue;
    private float _normalizedValue => (float)Math.Round((double)(_value / _maxValue), 2);

    private string _rawTextValue = "{0}/{1}";
    private Coroutine _healthChangeCoroutine;

    public void UpdateValue(float value, float maxValue)
    {
        if (maxValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        _value = value;
        _maxValue = maxValue;

        UpdateSlider();
    }

    private void UpdateSlider(float delta = 0)
    {
        if (_healthChangeCoroutine != null && _background.gameObject.activeSelf)
        {
            StopCoroutine(_healthChangeCoroutine);
        }
        _healthChangeCoroutine = StartCoroutine(ChangeSliderCoroutine(_normalizedValue));
    }

    private IEnumerator ChangeSliderCoroutine(float targetValue)
    {
        if (_foreground.fillAmount == targetValue)
        {
            UpdateText();
        }

        float maxDelta;
        if (targetValue == 0)
            maxDelta = 1;
        else
            maxDelta = Time.deltaTime * _fillSpeedRate;

        while (_foreground.fillAmount != targetValue)
        {
            _foreground.fillAmount = Mathf.MoveTowards(_foreground.fillAmount, targetValue, maxDelta);
            UpdateText();
            yield return null;
        }
        _healthChangeCoroutine = null;
    }

    private void UpdateText()
    {
        if (_showText)
            _textValue.text = string.Format(_rawTextValue, (int)_value, _maxValue);
        else if (_textValue.gameObject.activeInHierarchy)
            _textValue.gameObject.SetActive(false);

        if (_hideWhenFull)
        {
            _background.gameObject.SetActive(_value != _maxValue);
        }
    }
}
