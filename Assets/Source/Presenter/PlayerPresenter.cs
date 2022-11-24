using UnityEngine;

public class PlayerPresenter : CharacterPresenter<Player>
{
    [SerializeField] private PlayerInfoUI _playerInfoUI;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private FillBarUI _experienceBarUI;
    [SerializeField] private WeaponFactory _weaponFactory;

    private WeaponPresenter _currentWeapon;

    public override void Initialize(Player model, int level)
    {
        base.Initialize(model, level);

        _weaponFactory.Initialize(model, _weaponPoint);
        _currentWeapon = _weaponFactory.GetWeaponPresenter(model.CurrentWeapon);

        _playerInfoUI?.Initialize(model.Stats);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Model.ChangedWeapon += OnChangedWeapon;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Model.ChangedWeapon -= OnChangedWeapon;
    }

    protected override void Update()
    {
        base.Update();

        _experienceBarUI.UpdateValue(Model.Stats.CurrentExp, Model.Stats.ExpRequired.Value);
    }

    protected override void OnDying()
    {
        base.OnDying();
    }

    private void OnChangedWeapon(Weapon weapon)
    {
        _currentWeapon.Hide();
        _currentWeapon = _weaponFactory.GetWeaponPresenter(weapon);
        _currentWeapon.Show();
    }
}
