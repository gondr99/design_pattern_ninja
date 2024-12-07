using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FlipRenderer", story: "flip [renderer]", category: "Action", id: "af6b84944949124940261841339ea1cd")]
public partial class FlipRendererAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;

    protected override Status OnStart()
    {
        Renderer.Value.Flip();
        return Status.Success;
    }
}

