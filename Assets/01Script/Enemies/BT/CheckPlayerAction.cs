using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckPlayer", story: "[Entity] Check [target] in [radius]", category: "Action", id: "9943da4743be8d2875d42bc629019141")]
public partial class CheckPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Entity;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;

    protected override Status OnStart()
    {
        Target.Value = Entity.Value.GetTargetInRadius(Radius.Value);
        
        return Target.Value != null ? Status.Success : Status.Failure;
    }
}

