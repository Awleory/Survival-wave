using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementPhysics))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int BaseDamage;

    public UnityEvent HealthChanged;
    public MovementPhysics MovementPhysics { get; private set; }

    protected int Health;
    protected StateMachine StateMachine = new StateMachine();

    protected virtual void Awake()
    {
        MovementPhysics = GetComponent<MovementPhysics>();

        Health = MaxHealth;
    }

    protected virtual void OnEnable() { }
   
    protected virtual void OnDisable() { }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void FixedUpdate() { }

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            Health = Mathf.Max(0, Health - damage);
        }
    }

    public virtual void RestoreHealth(int healthPoints)
    {
        if (healthPoints > 0)
        {
            Health = Mathf.Min(MaxHealth, Health + healthPoints);
        }
    }
}
