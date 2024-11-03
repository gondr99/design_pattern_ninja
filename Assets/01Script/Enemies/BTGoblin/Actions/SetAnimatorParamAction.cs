using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAnimatorParam", story: "Set [Enemy] param [BoolParam] To [Value]", category: "Action", id: "de3def677545aba54ea0be5aa598dc53")]
public partial class SetAnimatorParamAction : Action
{
    [SerializeReference] public BlackboardVariable<AnimParamSO> CurrentParam;
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<AnimParamSO> BoolParam;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        var anim = Enemy.Value.GetCompo<EntityRenderer>();
        if (CurrentParam.Value != null)
            anim.SetParam(CurrentParam.Value, false);
        anim.SetParam(BoolParam.Value, Value);
        CurrentParam.Value = BoolParam.Value;
        return Status.Success;
    }

}

