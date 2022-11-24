using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CharacterPresenter<Player> _playerPresenter;
    [SerializeField] private int _damage;
    [SerializeField] private int _heal;

    public void DealDamage()
    {
        _playerPresenter.Model.ApplyDamage(_damage, true);
    }

    public void Heal()
    {
        _playerPresenter.Model.ApplyHeal(_heal);
    }

    public void UpLevel()
    {
        _playerPresenter.Model.Stats.UpLevel();
    }

    public void Restore()
    {
        _playerPresenter.Model.Health.Restore();
    }
}
