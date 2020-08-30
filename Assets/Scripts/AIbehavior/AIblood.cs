using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AIblood : Action
{
    public GameObject bloodSet;

    public override TaskStatus OnUpdate()
    {
        bloodSet.SetActive(true);
            return TaskStatus.Success;
    }
}
