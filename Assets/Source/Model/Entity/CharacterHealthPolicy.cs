using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthPolicy : HealthPolicy
{
    public override int CalculateDamage(int damage)
    {
        return damage;
    }

    public override int CalculateHeal(int healPoitns)
    {
        return healPoitns;
    }

    public override void CalculateMaxHealth()
    {
        
    }
}
