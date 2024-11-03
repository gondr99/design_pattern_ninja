using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsEntityHit", story: "Is Entity [Hit]", category: "Conditions", id: "1cf1a45b3ab76c0fca3c174ecf3d9331")]
public partial class IsEntityHitCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> Hit;

    public override bool IsTrue()
    {
        return Hit.Value;
    }

}
