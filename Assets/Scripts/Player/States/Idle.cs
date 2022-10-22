using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class Idle : State
    {
        new protected Player Owner;

        public Idle(StateMachine stateMachine, Player stateOwner) : base(stateMachine, stateOwner)
        {
            Owner = stateOwner;
        }
    }
}
