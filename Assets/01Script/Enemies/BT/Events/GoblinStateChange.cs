using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/GoblinStateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "GoblinStateChange", message: "change to [state]", category: "Events", id: "41febcfaf20f17cd0bd70328d72c8888")]
public partial class GoblinStateChange : EventChannelBase
{
    public delegate void GoblinStateChangeEventHandler(GoblinState state);
    public event GoblinStateChangeEventHandler Event; 

    public void SendEventMessage(GoblinState state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GoblinState> stateBlackboardVariable = messageData[0] as BlackboardVariable<GoblinState>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(GoblinState);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        GoblinStateChangeEventHandler del = (state) =>
        {
            BlackboardVariable<GoblinState> var0 = vars[0] as BlackboardVariable<GoblinState>;
            if(var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as GoblinStateChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as GoblinStateChangeEventHandler;
    }
}

