using UnityEngine;

public class PresentersFactory : MonoBehaviour
{
    [SerializeField] private EnemyPresenter _bat;

    private Transform _defaultParent;

    public void Initialize(Transform defaultParent)
    {
        _defaultParent = defaultParent;
    }

    public EnemyPresenter CreateBat(SimpleEnemy batModel, Vector2 position)
    {
        var bat = Instantiate(_bat, position, Quaternion.identity, _defaultParent);
        bat.Initialize(batModel);
        bat.EndInitialize();

        return bat;
    }
}
