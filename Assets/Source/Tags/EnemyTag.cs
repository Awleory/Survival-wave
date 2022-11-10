using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    public Enemy Model { get; private set; }

    public void Initialize(Enemy model)
    {
        Model = model;
    }
}
