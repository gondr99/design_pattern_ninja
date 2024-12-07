using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatrolTo", story: "[mover] patrol to [renderer] with [sec]", category: "Action", id: "de3c53a7eebb04ae162567c5ec283fdb")]
public partial class PatrolToAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
    [SerializeReference] public BlackboardVariable<float> Sec;

    private float _patrolStartTime;
    
    protected override Status OnStart()
    {
        float xDirection = Renderer.Value.FacingDirection;
        Mover.Value.SetMovement(xDirection);
        _patrolStartTime = Time.time;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(_patrolStartTime + Sec.Value <= Time.time)
            return Status.Success;
        
        if(Mover.Value.IsGrounded == false)
            return Status.Success;
        
        return Status.Running;
    }
}

