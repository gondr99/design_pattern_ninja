using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopMover", story: "stop [mover] with [yAxis]", category: "Action", id: "dd9d76000dc4620099762fe3cca37ec4")]
public partial class StopMoverAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<bool> YAxis;

    protected override Status OnStart()
    {
        Mover.Value.StopImmediately(YAxis.Value);
        return Status.Success;
    }
}

