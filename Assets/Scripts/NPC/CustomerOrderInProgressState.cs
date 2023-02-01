using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderInProgressState : CustomerState
{
    public void Enter(CustomerAgent agent)
    {
        agent.patienceTimer.StartTimer();
    }

    public void Exit(CustomerAgent agent)
    {
        agent.stateMachine.ChangeState(CustomerStateId.Exit);
    }

    public CustomerStateId GetId()
    {
        return CustomerStateId.OrderInProgress;
    }

    public void Update(CustomerAgent agent)
    {
        if (agent.patienceTimer.active == false)
        {
            Exit(agent);
        }
    }
}
