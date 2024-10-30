using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseToTarget", story: "[Enemy] chase to [target] when distance to [distance]", category: "Action", id: "51857ecef4a5a0f2a0086e5e9d548d0a")]
public partial class ChaseToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<Player> Target;
    [SerializeReference] public BlackboardVariable<float> Distance;
    protected override Status OnUpdate()
    {
        float directionX = Mathf.Sign( Target.Value.transform.position.x - Enemy.Value.transform.position.x);
        Enemy.Value.GetCompo<EntityMover>().SetMovement(directionX);

        float distance = Vector2.Distance(Target.Value.transform.position, Enemy.Value.transform.position);
        if (distance <= Distance.Value)
        {
            return Status.Success;
        }
        return Status.Running;
    }
}

