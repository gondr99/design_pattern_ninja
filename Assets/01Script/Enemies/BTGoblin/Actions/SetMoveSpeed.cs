using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetMoveSpeed", story: "Set [Entity] SpeedMultiplier to [Value]", category: "Action", id: "84affc89d3e31efc5822198232f1079b")]
public partial class SetMoveSpeedAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;
    [SerializeReference] public BlackboardVariable<float> Value;
    protected override Status OnStart()
    {
        var mover = Entity.Value.GetCompo<EntityMover>();
        mover.SetMoveSpeedMultiplier(Value.Value);
        return Status.Success;
    }
}

