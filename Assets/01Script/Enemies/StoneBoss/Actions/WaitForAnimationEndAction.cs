using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimationEnd", story: "Waiting [EntityAnimator] End to clip", category: "Action", id: "6d45292e8eb34da080f3cc1fe7fbb5ed")]
public partial class WaitForAnimationEndAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;

    private bool _isAnimtionEnd;
    
    protected override Status OnStart()
    {
        _isAnimtionEnd = false;
        EntityAnimator.Value.OnAnimationEnd += HandleAnimationEnd;
        return Status.Running;
    }

    private void HandleAnimationEnd()
    {
        _isAnimtionEnd = true;
    }
    
    protected override Status OnUpdate()
    {
        return _isAnimtionEnd ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        EntityAnimator.Value.OnAnimationEnd -= HandleAnimationEnd;
    }
}

