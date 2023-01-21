using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderInProgressState : CustomerState
{
    public void Enter(CustomerAgent agent)
    {
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
    }
}
