using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseToTarget", story: "[Entity] chase to [target]", category: "Action", id: "5d83a71bde474fee2e6d37809a1445fa")]
public partial class ChaseToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Entity;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    private EntityRenderer _renderer;
    private EntityMover _mover;
    protected override Status OnStart()
    {
        _renderer = Entity.Value.GetCompo<EntityRenderer>();
        _mover = Entity.Value.GetCompo<EntityMover>();
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 targetPos = Target.Value.position;
        Vector3 myPos = Entity.Value.transform.position;

        float xDirection = Mathf.Sign((targetPos - myPos).x);
        
        _mover.SetMovement(xDirection);
        _renderer.FlipController(xDirection);
        
        return Status.Running;
    }

}

