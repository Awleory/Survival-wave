using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    private PlayerController _controller;

    public PlayerStates.Idle IdleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _controller = new PlayerController(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        IdleState = new PlayerStates.Idle(StateMachine, this);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        _controller.UpdateMove();
    }
}

