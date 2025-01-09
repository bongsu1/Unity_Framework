using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : Enum
{
    Dictionary<T, BaseState<T>> _states = new Dictionary<T, BaseState<T>>();
    BaseState<T> _curState = null;

    public void AddState(T stateEnum, BaseState<T> state)
    {
        state.SetStateMachine(this);
        _states.Add(stateEnum, state);
    }

    public void Start(T stateEnum)
    {
        _curState = _states[stateEnum];
        _curState.Enter();
    }

    public void Update()
    {
        _curState.Update();
        _curState.Transition();
    }

    public void LateUpdate()
    {
        _curState.LateUpdate();
    }

    public void FixedUpdate()
    {
        _curState.FixedUpdate();
    }

    public void ChangeState(T stateEnum)
    {
        _curState.Exit();
        _curState = _states[stateEnum];
        _curState.Enter();
    }
}

public class BaseState<T> where T : Enum
{
    StateMachine<T> _stateMachine = null;

    public void SetStateMachine(StateMachine<T> stateMachine)
    {
        _stateMachine = stateMachine;
    }

    protected void ChangeState(T stateEnum)
    {
        _stateMachine.ChangeState(stateEnum);
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }

    public virtual void Transition() { }
}

