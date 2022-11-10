using TMPro;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stats;

    private Stats _playerStats;
    private AttributeBonuses _attributeBonuses;

    private const string rawStatText = "{0}: {1}; {2}: {3}";
    private const string rawLevelText = "Level - {0}";

    public void Initialize(Stats playerStats, AttributeBonuses attributeBonuses)
    {
        _playerStats = playerStats;
        _attributeBonuses = attributeBonuses;

        OnUpdate();
        enabled = true;
    }

    private void OnEnable()
    {
        _playerStats.Updated += OnUpdate;
    }

    private void OnDisable()
    {
        _playerStats.Updated -= OnUpdate;
    }

    private void OnUpdate()
    {
        _stats.text = string.Format(rawLevelText, _playerStats.Level) + "\n" +
            string.Format(rawStatText, "���������", _playerStats.Vitality.Value, "����� ��", _attributeBonuses.Health) + "\n" +
            string.Format(rawStatText, "����", _playerStats.Strength.Value, "������������� ��� ������", _attributeBonuses.PhysicalResist) + "\n" +
            string.Format(rawStatText, "��������", _playerStats.Vitality.Value, "����� �������� ����", _attributeBonuses.Speed) + "\n" +
            string.Format(rawStatText, "���������", _playerStats.Vitality.Value, "������������� ���. ������/����� �������", _attributeBonuses.MagicResist + "/" + _attributeBonuses.SelfHealing);
    }
}
