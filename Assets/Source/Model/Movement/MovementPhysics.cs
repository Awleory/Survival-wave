using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPhysics : IUpdateble
{
    [SerializeField] private float _speed = 10;

    private Vector2 _directionVelocity;
    private Rigidbody2D _rigidbody2D;

    public MovementPhysics(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }

    public void Update(float deltaTime)
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _directionVelocity * _speed * deltaTime);
    }

    public void Move(Vector2 direction)
    {
        _directionVelocity = direction;
    }

    public void FixedUpdate(float deltaTime) { }
}
