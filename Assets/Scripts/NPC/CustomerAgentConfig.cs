using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CustomerAgentConfig : ScriptableObject
{
    //any customizable things in states go here, can reference in states using agent.config
    //example public int speed = 0;

    public float walkSpeed = 3f;
    public float patienceTime = 10f;

}
