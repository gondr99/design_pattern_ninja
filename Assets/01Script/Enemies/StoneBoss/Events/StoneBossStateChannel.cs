using System;
using Unity.Behavior.GraphFramework;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StoneBossStateChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StoneBossStateChannel", message: "Stoneboss change state to [state]", category: "Events", id: "8821a4d5d93a7d6858479465a641150f")]
public partial class StoneBossStateChannel : EventChannelBase
{
    public delegate void StoneBossStateChannelEventHandler(StoneBossState state);
    public event StoneBossStateChannelEventHandler Event; 

    public void SendEventMessage(StoneBossState state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<StoneBossState> stateBlackboardVariable = messageData[0] as BlackboardVariable<StoneBossState>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(StoneBossState);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        StoneBossStateChannelEventHandler del = (state) =>
        {
            BlackboardVariable<StoneBossState> var0 = vars[0] as BlackboardVariable<StoneBossState>;
            if(var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StoneBossStateChannelEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StoneBossStateChannelEventHandler;
    }
}

