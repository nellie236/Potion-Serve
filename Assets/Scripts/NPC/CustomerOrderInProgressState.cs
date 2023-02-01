using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderInProgressState : CustomerState
{

    public void Enter(CustomerAgent agent)
    {
        agent.orderFulfilled = false;
        agent.patienceTimer.StartTimer();
    }

    public void Exit(CustomerAgent agent)
    {
        
    }

    public CustomerStateId GetId()
    {
        return CustomerStateId.OrderInProgress;
    }

    public void Update(CustomerAgent agent)
    {
        if (agent.orderFulfilled)
        {
            agent.dialogueManager.orderSuccess();
            agent.stateMachine.ChangeState(CustomerStateId.Exit);
        }

        if (!agent.patienceTimer.active && !agent.orderFulfilled)
        {
            agent.dialogueManager.orderFail();
            agent.stateMachine.ChangeState(CustomerStateId.Exit);
        }
    }
}
