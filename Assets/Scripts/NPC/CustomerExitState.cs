using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerExitState : CustomerState
{
    public void Enter(CustomerAgent agent)
    {
    }

    public void Exit(CustomerAgent agent)
    {
    }

    public CustomerStateId GetId()
    {
        return CustomerStateId.Exit;
    }

    public void Update(CustomerAgent agent)
    {
        agent.transform.localRotation = Quaternion.Euler(0, 180, 0);
        agent.myRB.velocity = new Vector2(-agent.config.walkSpeed, 0);
    }

    
}