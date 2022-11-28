using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] List<WeaponPresenter> _weaponTemplates;

    private List<WeaponPresenter> _weaponPresenters = new List<WeaponPresenter>();

    public void Initialize(Player player, Transform weaponPoint, Transform _bulletsContainer)
    {
        foreach (var weapon in _weaponTemplates)
        {
            WeaponPresenter weaponPresenter = Instantiate(weapon, weaponPoint);
            _weaponPresenters.Add(weaponPresenter);
            weaponPresenter.Initialize(new Weapon(), _bulletsContainer);
            player.GiveWeapon(weaponPresenter.Model);
            weaponPresenter.Hide();
        }

        WeaponPresenter currentWeapon = GetWeaponPresenter(player.CurrentWeapon);
        if (currentWeapon != null)
            currentWeapon.Show();
    }

    public WeaponPresenter GetWeaponPresenter(Weapon weapon)
    {
        return _weaponPresenters.Find(weaponPresenter => weaponPresenter.Model == weapon);
    }
}
