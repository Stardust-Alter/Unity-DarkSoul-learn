using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class AICondition : Conditional
{
    public SharedGameObject targetObj;
    bool isInBattle;

    public override TaskStatus OnUpdate()
    {
        if (true)
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }
}
