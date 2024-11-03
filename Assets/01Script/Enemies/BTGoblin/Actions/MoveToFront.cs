using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToFront", story: "[Enemy] Move to Front in [Second] sec", category: "Action", id: "34787b490d35cc75ebf55d81a6e24541")]
public partial class MoveToFrontAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<float> Second;

    private float _startTime = 0;
    protected override Status OnStart()
    {
        _startTime = Time.time;
        var mover = Enemy.Value.GetCompo<EntityMover>();
        float frontDirection = Enemy.Value.transform.right.x;
        mover.SetMovement(frontDirection);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_startTime + Second.Value < Time.time)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

