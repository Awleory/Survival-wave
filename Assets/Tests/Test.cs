using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private EntityPresenter<Entity<Health>> _entityPresenter;
    [SerializeField] private int _damage;
    [SerializeField] private int _heal;

    public void DealDamage()
    {
        _entityPresenter.Model.ApplyDamage(_damage, DamageType.Pure);
    }

    public void Heal()
    {
        _entityPresenter.Model.ApplyHeal(_heal);
    }
}
