using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerStateId
{
    Enter,
    Queue,
    OrderInProgress,
    Exit,
    Idle
}
public interface CustomerState 
{
    CustomerStateId GetId();
    void Enter(CustomerAgent agent);
    void Update(CustomerAgent agent);
    void Exit(CustomerAgent agent);
}
