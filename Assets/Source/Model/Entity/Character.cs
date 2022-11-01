using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public CharacterStats Stats { get; private set; }

    public Character(HealthPolicy healthPolicy) : base(healthPolicy)
    {
        Stats = new CharacterStats();
    }
}
