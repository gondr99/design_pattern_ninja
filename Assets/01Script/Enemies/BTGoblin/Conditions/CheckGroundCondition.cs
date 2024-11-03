using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckGround", story: "[Enemy] is not on ground", category: "Conditions", id: "288833d82fbbfb49590b4f097cf6c5f7")]
public partial class CheckGroundCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;

    public override bool IsTrue()
    {
        return Enemy.Value.GetCompo<EntityMover>().IsGrounded == false;
    }

}
