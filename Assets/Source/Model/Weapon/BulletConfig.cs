using UnityEngine;

public class BulletConfig : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField] private DamageType _damageType = DamageType.Physical;
    [SerializeField, Min(1)] private int _maxTargetHits = 1;

    public float Damage => _damage;
    public int MaxTargetHits => _maxTargetHits;
    public DamageType DamageType => _damageType;
    public float Speed => _speed;    
}
