using System;
using System.Collections.Generic;
using SM;
using UnityEngine;

internal class WaitingForLineState : IState 
{
    private StateMachine _stateMachine;
    private GameObject[] _characters;
    private List<Vector3> _points;
    private Func<bool> _condition;
    private IState _waiting;

    public WaitingForLineState(StateMachine stateMachine,
        Func<bool> condition,
        GameObject[] characters,
        List<Vector3> points,
        IState waiting)
    {
        _stateMachine = stateMachine;
        _characters = characters;
        _points = points;
        _condition = condition;
        _waiting = waiting;
    }

    public void Enter()
    {
    }
    

    public void Update()
    {
        if (_condition.Invoke())
        {
            _stateMachine.SetState(_waiting); 
        }
    }

    public void Exit()
    {
    }

    
}