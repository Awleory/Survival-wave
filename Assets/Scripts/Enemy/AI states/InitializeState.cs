using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyState
{
    public class Initialize : State
    {
        public Initialize(StateMachine stateMachine, Entity stateOwner) : base(stateMachine, stateOwner)
        {
        }
    }
}
