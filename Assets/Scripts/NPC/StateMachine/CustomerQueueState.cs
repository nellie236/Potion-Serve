using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueState : CustomerState
{
    //float waitTime;
    public void Enter(CustomerAgent agent)
    {
        //waitTime = agent.config.waitTime;
        agent.waitTimer.ourCustomer(agent.gameObject);
        agent.waitTimer.StartWaitTimer();
    }

    public void Exit(CustomerAgent agent)
    {
        
    }

    public CustomerStateId GetId()
    {
        return CustomerStateId.Queue;
    }

    public void Update(CustomerAgent agent)
    {
        if (agent.atShop)
        {
            if (agent.dialogueManager.isActive && agent.waitTimer.hasPatience)
            {
                if (agent.dialogueTrigger.whichMessages == 1)
                {
                    agent.stateMachine.ChangeState(CustomerStateId.OrderInProgress);
                }
                else if (agent.dialogueTrigger.whichMessages == 2)
                {
                    agent.stateMachine.ChangeState(CustomerStateId.Exit);
                }
            }
            else
            { 
                if (!agent.waitTimer.hasPatience)
                {
                    agent.stateMachine.ChangeState(CustomerStateId.Exit);
                }
            }
        }
    }
}
