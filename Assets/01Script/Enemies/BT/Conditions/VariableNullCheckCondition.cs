using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "VariableNullCheck", story: "[variable] is null [status]", category: "Conditions", id: "de558f6f73e536b571de1302292c8ba7")]
public partial class VariableNullCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Variable;
    [SerializeReference] public BlackboardVariable<bool> Status;

    public override bool IsTrue()
    {
        return Status.Value ? Variable.Value == null : Variable.Value != null;
    }
}
