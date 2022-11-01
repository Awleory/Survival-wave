using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private EntityPresenter<Player, HealthPolicy> _entityPresenter;
    [SerializeField] private int _damage;
    [SerializeField] private int _heal;

    public void DealDamage()
    {
        _entityPresenter.Model.ApplyDamage(_damage);
    }

    public void Heal()
    {
        _entityPresenter.Model.Heal(_heal);
    }
}
