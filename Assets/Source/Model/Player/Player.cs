using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    private Controller _controller;

    public Player(CharacterHealthPolicy healthPolicy) : base(healthPolicy)
    {
        _controller = new Controller(this);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _controller.Update(deltaTime);
    }
}

