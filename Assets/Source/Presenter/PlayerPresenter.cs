using UnityEngine;

public class PlayerPresenter : CharacterPresenter<Player>
{
    [SerializeField] private PlayerInfoUI _playerInfoUI;
    [SerializeField] private WeaponPresenter _weaponTemplate;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private FillBarUI _experienceBarUI;

    public override void Initialize(Player model)
    {
        base.Initialize(model);

        WeaponPresenter weaponPresenter = Instantiate(_weaponTemplate, _weaponPoint);
        weaponPresenter.Initialize(model.Weapon);
        weaponPresenter.EndInitialize();

        _playerInfoUI?.Initialize(model.Stats, model.AttributeBonuses);
    }

    protected override void Update()
    {
        base.Update();

        _experienceBarUI.UpdateValue(Model.Stats.CurrentExp, Model.Stats.ExpForNextLevel);
    }

    protected override void OnDied()
    {
        Debug.Log("ты сдох лох");
    }
}
