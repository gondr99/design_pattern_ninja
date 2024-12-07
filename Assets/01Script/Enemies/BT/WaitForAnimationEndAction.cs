using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimationEnd", story: "wait end [trigger]", category: "Action", id: "4d6aa371be2c1382bf15fa6f7b69ce3d")]
public partial class WaitForAnimationEndAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimator> Trigger;

    private bool _isTrigger;
    protected override Status OnStart()
    {
        _isTrigger = false;
        Trigger.Value.OnAnimationEnd += HandleAnimationEnd;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return _isTrigger ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEnd -= HandleAnimationEnd;
    }

    private void HandleAnimationEnd()
    {
        _isTrigger = true;
    }
}

