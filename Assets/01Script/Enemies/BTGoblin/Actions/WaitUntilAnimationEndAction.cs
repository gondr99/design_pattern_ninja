using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitUntilAnimationEnd", story: "Wait Until [target] Animation end", category: "Action", id: "f3decde259642cbfa0c17a69c6bf3953")]
public partial class WaitUntilAnimationEndAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Target;
    
    private EntityAnimator _entityAnimator;
    private bool _isAnimtionEnd;
    protected override Status OnStart()
    {
        _entityAnimator = Target.Value.GetCompo<EntityAnimator>();
        _isAnimtionEnd = false;
        _entityAnimator.OnAnimationEnd += HandleAnimationEnd;
        return Status.Running;
    }

    private void HandleAnimationEnd()
    {
        _isAnimtionEnd = true;
    }

    protected override Status OnUpdate()
    {
        if (_isAnimtionEnd)
            return Status.Success;
        else
            return Status.Running;
    }

    protected override void OnEnd()
    {
        _entityAnimator.OnAnimationEnd -= HandleAnimationEnd;
    }
}

