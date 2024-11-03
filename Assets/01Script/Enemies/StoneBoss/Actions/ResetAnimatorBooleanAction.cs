using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ResetAnimatorBoolean", story: "Reset [paramName] in [animator] boolean", category: "Action", id: "91b18c9cf8e279ae5512900fb3d7235b")]
public partial class ResetAnimatorBooleanAction : Action
{
    [SerializeReference] public BlackboardVariable<string> ParamName;
    [SerializeReference] public BlackboardVariable<Animator> Animator;

    protected override Status OnStart()
    {
        if (string.IsNullOrEmpty(ParamName.Value) == false)
        {
            Animator.Value.SetBool(ParamName.Value, false);
        }
        return Status.Success;
    }

}

