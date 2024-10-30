using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PlayerSpotted", story: "[Enemy] Spot Player And Write to [Target]", category: "Conditions", id: "ddae846f3edb0ef8c162721dfd17a8bb")]
public partial class PlayerSpottedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;
    [SerializeReference] public BlackboardVariable<Player> Target;

    public override bool IsTrue()
    {
        RaycastHit2D hit = Enemy.Value.CheckPlayerInRange();

        if (hit.collider != null)
        {
            Target.Value = hit.collider.GetComponent<Player>();
        }
        
        return hit.collider != null;
    }
}
