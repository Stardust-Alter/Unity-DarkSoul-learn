using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AICalculatePower : Action
{
    public SharedInt selfPower;

    public override TaskStatus OnUpdate()
    {
        if (true)
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }
}
