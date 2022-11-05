using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player<THealth> : Character<THealth> where THealth : CharacterHealth
{
    private Controller _controller;

    public Player(THealth health) : base(health) 
    { 
        _controller = new Controller(Movement); 
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _controller.Update(deltaTime);
    }
}

