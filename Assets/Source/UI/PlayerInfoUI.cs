using TMPro;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stats;
    [SerializeField] private TextMeshProUGUI _bonuses;
    [SerializeField] private TextMeshProUGUI _levelInfo;

    private Stats _playerStats;


    private const string _rawLevelInfoText = "Level - {0}";

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
        _levelInfo.text = string.Format(_rawLevelInfoText, _playerStats.Level);
    }
}
