using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FlipEnemy", story: "[Enemy] Flip", category: "Action", id: "8e5b21d795d6267af1434fd593361bbd")]
public partial class FlipEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;

    protected override Status OnStart()
    {
        Enemy.Value.GetCompo<EntityRenderer>().Flip();
        return Status.Success;
    }
}

