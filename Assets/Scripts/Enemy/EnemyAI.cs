using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private StateMachine _stateMachine;

    private Enemy Enemy 

    private void Awake()
    {
        _stateMachine = new StateMachine();
    }

    private void OnEnable()
    {
        _stateMachine.Initialize();
    }
}
