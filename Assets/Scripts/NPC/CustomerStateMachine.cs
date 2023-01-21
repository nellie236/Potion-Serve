using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateMachine 
{
    public CustomerState[] states;
    public CustomerAgent agent;
    public CustomerStateId currentState;

    public CustomerStateMachine(CustomerAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(CustomerStateId)).Length;
        states = new CustomerState[numStates];
    }

    public void RegisterState(CustomerState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public CustomerState GetState(CustomerStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(CustomerStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
