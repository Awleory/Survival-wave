using UnityEngine;

public class EnemyPresentersFactory : MonoBehaviour
{
    [SerializeField] private CharacterPresenter<SimpleEnemy> _simpleEnemyTemplate;

    public CharacterPresenter<SimpleEnemy> CreateSimpleEnemy(SimpleEnemy enemy)
    {
        return CreatePresenter(_simpleEnemyTemplate, enemy);
    }

    private CharacterPresenter<SimpleEnemy> CreatePresenter(CharacterPresenter<SimpleEnemy> enemyTemplate, SimpleEnemy model)
    {
        CharacterPresenter<SimpleEnemy> enemyPresenter = Instantiate(enemyTemplate);
        enemyPresenter.Initialize(model);
        enemyPresenter.EndInitialize();
        return enemyPresenter;
    }
}
