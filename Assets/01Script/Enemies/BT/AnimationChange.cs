using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/AnimationChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "AnimationChange", message: "change to [param]", category: "Events", id: "17c87ea5fc952b43eded27be65264058")]
public partial class AnimationChange : EventChannelBase
{
    public delegate void AnimationChangeEventHandler(string param);
    public event AnimationChangeEventHandler Event; 

    public void SendEventMessage(string param)
    {
        Event?.Invoke(param);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<string> paramBlackboardVariable = messageData[0] as BlackboardVariable<string>;
        var param = paramBlackboardVariable != null ? paramBlackboardVariable.Value : default(string);

        Event?.Invoke(param);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        AnimationChangeEventHandler del = (param) =>
        {
            BlackboardVariable<string> var0 = vars[0] as BlackboardVariable<string>;
            if(var0 != null)
                var0.Value = param;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as AnimationChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as AnimationChangeEventHandler;
    }
}

