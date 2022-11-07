using TMPro;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stats;

    private Stats _playerStats;

    private const string rawInfoText = 
        "Level - {0}\n" +
        "Vitality - {1}\n" +
        "Strength - {2}\n" +
        "Dexterity - {3}\n" +
        "Intellect - {4}";

    public void Initialize(Stats playerStats)
    {
        _playerStats = playerStats;
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
       _stats.text = string.Format(rawInfoText, 
           _playerStats.Level,
           _playerStats.Vitality.Value,
           _playerStats.Strength.Value,
           _playerStats.Dexterity.Value,
           _playerStats.Intellect.Value);
    }
}
