using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopEnemy", story: "Stop [Enemy]", category: "Action", id: "8db516c156849315cb4938a2b6113a01")]
public partial class StopEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;

    protected override Status OnStart()
    {
        var mover = Enemy.Value.GetCompo<EntityMover>();
        mover.StopImmediately(false);
        return Status.Success;
    }

    
}

