
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerEnterState : CustomerState
{
    public void Enter(CustomerAgent agent)
    {
        //agent.dialogueTrigger.whichMessages = 0;
        string payment = agent.config.coinAmount.ToString();
        agent.dialogueManager.coinPaymentAmount.text = payment;
    }

    public void Exit(CustomerAgent agent)
    {
        
    }

    public CustomerStateId GetId()
    {
        return CustomerStateId.Enter;
    }

    public void Update(CustomerAgent agent)
    {
        //here put what I want to happen when this state starts, aka have customer move towards shop
        if (!agent.atShop)
        {
            agent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            agent.myRB.velocity = new Vector2(agent.config.walkSpeed, 0);
        }
        else if (agent.atShop)
        {
            agent.stateMachine.ChangeState(CustomerStateId.Queue);
        }
    }
}
