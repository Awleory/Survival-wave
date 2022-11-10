using UnityEngine;

public class PlayerPresenter : CharacterPresenter<Player>
{
    [SerializeField] private PlayerInfoUI _playerInfoUI;

    public override void Initialize(Player model)
    {
        base.Initialize(model);

        _playerInfoUI.Initialize(model.Stats, model.AttributeBonuses);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyTag>(out EnemyTag enemyTag))
        {
            Model.ApplyDamage(enemyTag.Model.Damage, DamageType.Physical);
            Debug.Log("damage " + enemyTag.Model.Damage);
        }
    }
}
