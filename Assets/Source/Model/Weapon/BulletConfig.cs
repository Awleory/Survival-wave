using UnityEngine;

public class BulletConfig : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0)] private bool _isPureDamage;
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(1)] private int _maxTargetHits = 1;

    public float Damage => _damage;
    public int MaxTargetHits => _maxTargetHits;
    public bool IsPureDamage  => _isPureDamage;
    public float Speed => _speed;    
}
