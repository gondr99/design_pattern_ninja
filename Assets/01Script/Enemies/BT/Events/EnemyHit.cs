using System;
using Unity.Behavior.GraphFramework;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/EnemyHit")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "EnemyHit", message: "[Target] Hit", category: "Events", id: "90fe4c9bf4d4a8654f1a85ceeb558d7c")]
public partial class EnemyHit : EventChannelBase
{
    public delegate void EnemyHitEventHandler(Entity Target);
    public event EnemyHitEventHandler Event; 

    public void SendEventMessage(Entity Target)
    {
        Event?.Invoke(Target);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<Entity> TargetBlackboardVariable = messageData[0] as BlackboardVariable<Entity>;
        var Target = TargetBlackboardVariable != null ? TargetBlackboardVariable.Value : default(Entity);

        Event?.Invoke(Target);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        EnemyHitEventHandler del = (Target) =>
        {
            BlackboardVariable<Entity> var0 = vars[0] as BlackboardVariable<Entity>;
            if(var0 != null)
                var0.Value = Target;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as EnemyHitEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as EnemyHitEventHandler;
    }
}

