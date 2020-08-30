using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Bossattack_1 : Action
{

    public SharedGameObject targetGameObject;

    private Animator animator;
    private GameObject prevGameObject;

    public override void OnStart()
    {
        var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
        if (currentGameObject != prevGameObject)
        {
            animator = currentGameObject.GetComponent<Animator>();
            prevGameObject = currentGameObject;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return TaskStatus.Failure;
        }

        animator.SetTrigger("attack1");

        return TaskStatus.Success;
    }

    public override void OnReset()
    {
        targetGameObject = null;
    }
}
