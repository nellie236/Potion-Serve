using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerIdleState : CustomerState
{
    public void Enter(CustomerAgent agent)
    {
    }

    public void Exit(CustomerAgent agent)
    {
    }

    public CustomerStateId GetId()
    {
        return CustomerStateId.Idle;
    }

    public void Update(CustomerAgent agent)
    {
    }
}
