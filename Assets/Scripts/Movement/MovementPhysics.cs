using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementPhysics : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _velocity;
    private Vector2 _directionVelocity;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        UpdateLogic();
    }

    private void FixedUpdate()
    {
        UpdatePhysics();   
    }

    public void Move(Vector2 direction)
    {
        _directionVelocity = direction;
    }

    public void ChaseTarget(Entity entity)
    {

    }

    private void UpdateLogic()
    {
    }

    private void UpdatePhysics()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _directionVelocity * _speed * Time.deltaTime);
    }
}
