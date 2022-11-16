using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stats;
    [SerializeField] private TextMeshProUGUI _bonuses;

    private Stats _playerStats;
    private AttributeBonuses _attributeBonuses;

    private const string rawStatText = "{0} - {1}";
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
        _stats.text = string.Format(rawStatText, "Живучесть", _playerStats.Vitality.Value) + "\n" +
            string.Format(rawStatText, "Сила", _playerStats.Strength.Value) + "\n" +
            string.Format(rawStatText, "Ловкость", _playerStats.Dexterity.Value) + "\n" +
            string.Format(rawStatText, "Интеллект", _playerStats.Intellect.Value);

        _bonuses.text = string.Format(rawLevelText, _playerStats.Level) + "\n" +
            string.Format(rawStatText, "Speed", _attributeBonuses.Speed) + "\n" +
            string.Format(rawStatText, "AttackSpeed", _attributeBonuses.AttackSpeed) + "\n" +
            string.Format(rawStatText, "Health", _attributeBonuses.Health) + "\n" +
            string.Format(rawStatText, "SelfHealing", _attributeBonuses.SelfHealing) + "\n" +
            string.Format(rawStatText, "Damage", _attributeBonuses.Damage) + "\n" +
            string.Format(rawStatText, "PhysicalResist", _attributeBonuses.PhysicalResist) + "\n" +
            string.Format(rawStatText, "MagicResist", _attributeBonuses.MagicResist) + "\n";
    }
}
