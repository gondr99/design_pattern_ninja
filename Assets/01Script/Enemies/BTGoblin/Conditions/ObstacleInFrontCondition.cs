using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ObstacleInFront", story: "Obstacle in front of [target]", category: "Conditions", id: "186197b774b51106026b8fae6ed06167")]
public partial class ObstacleInFrontCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Enemy> Target;

    public override bool IsTrue()
    {
        var hit = Target.Value.CheckObstacleInFront();
        
        return hit.collider != null;
    }
}
