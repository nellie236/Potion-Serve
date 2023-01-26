using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueState : CustomerState
{
    float waitTime;
    public void Enter(CustomerAgent agent)
    {
       
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
        if (agent.dialogueManager.isActive)
        {
            //switch state when decision is made
            //Debug.Log("spokento");
            if (agent.dialogueTrigger.whichMessages == 1)
            {
                agent.stateMachine.ChangeState(CustomerStateId.OrderInProgress);
            }
            else if (agent.dialogueTrigger.whichMessages == 2)
            {
                agent.stateMachine.ChangeState(CustomerStateId.Exit);
            }
        }
        else if (!agent.dialogueManager.isActive)
        {
            waitTime = agent.config.patienceTime;
            waitTime -= Time.deltaTime;

            if (waitTime <= 0.0f)
            {
                //switch state to Leave
                agent.stateMachine.ChangeState(CustomerStateId.Exit);
            }
        }
    }

 
}
