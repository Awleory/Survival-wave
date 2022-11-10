using UnityEngine;

[RequireComponent(typeof(EnemyTag))]
public class SimpleEnemyPresenter : CharacterPresenter<SimpleEnemy>
{
    public override void Initialize(SimpleEnemy model)
    {
        base.Initialize(model);
        GetComponent<EnemyTag>().Initialize(model);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
