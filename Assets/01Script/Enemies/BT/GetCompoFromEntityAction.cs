using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetCompoFromEntity", story: "get compo from [entity]", category: "Action", id: "92636df159cded515b3da121f234cb77")]
public partial class GetCompoFromEntityAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Entity;

    protected override Status OnStart()
    {
        BTEnemy enemy = Entity.Value;

        enemy.SetVariable("Mover", enemy.GetCompo<EntityMover>());
        enemy.SetVariable("Renderer", enemy.GetCompo<EntityRenderer>());
        enemy.SetVariable("MainAnimator", enemy.GetCompo<EntityRenderer>().Anim);
        enemy.SetVariable("AnimTrigger", enemy.GetCompo<EntityAnimator>());
        
        return Status.Success;
    }
}

