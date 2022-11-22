using UnityEngine;

public class PlayerPresenter : CharacterPresenter<Player>
{
    [SerializeField] private PlayerInfoUI _playerInfoUI;
    [SerializeField] private WeaponPresenter _weaponTemplate;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private FillBarUI _experienceBarUI;

    public override void Initialize(Player model, int level)
    {
        base.Initialize(model, level);

        WeaponPresenter weaponPresenter = Instantiate(_weaponTemplate, _weaponPoint);
        weaponPresenter.Initialize(model.Weapon);
        weaponPresenter.EndInitialize();

        _playerInfoUI?.Initialize(model.Stats);
    }

    protected override void Update()
    {
        base.Update();

        _experienceBarUI.UpdateValue(Model.Stats.CurrentExp, Model.Stats.ExpRequired.Value);

        Debugger.UpdateText("Level", Model.Stats.Level);
        Debugger.UpdateText("stats", Model.Stats.GetStatsInfo());
        Debugger.UpdateText("DamageResist", Model.Stats.DamageResist);
        Debugger.UpdateText("AttacksPerSecond", Model.Stats.AttacksPerSecond);
    }

    protected override void OnDying()
    {
        Debug.Log("ты сдох лох");
    }
}
