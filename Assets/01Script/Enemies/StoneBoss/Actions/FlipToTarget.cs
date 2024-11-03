using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FlipToTarget", story: "[enemy] flip to [target]", category: "Action", id: "5cd4f321ec9b7c89a45c17dc30a12c02")]
public partial class FlipToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Enemy;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    protected override Status OnStart()
    {
        float xDirection = Mathf.Sign( (Target.Value.position - Enemy.Value.transform.position).x );
        Enemy.Value.GetCompo<EntityRenderer>().FlipController(xDirection);
        return Status.Success;
    }
}

