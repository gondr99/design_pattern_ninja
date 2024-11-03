using System;
using Unity.Behavior.GraphFramework;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/CharacterStateChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "CharacterStateChannel", message: "Boss State Change to [newState]", category: "Events", id: "047cee79a98d8a3331505fc8e90a656e")]
public partial class CharacterStateChannel : EventChannelBase
{
    public delegate void CharacterStateChannelEventHandler(CharacterState newState);
    public event CharacterStateChannelEventHandler Event; 

    public void SendEventMessage(CharacterState newState)
    {
        Event?.Invoke(newState);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<CharacterState> newStateBlackboardVariable = messageData[0] as BlackboardVariable<CharacterState>;
        var newState = newStateBlackboardVariable != null ? newStateBlackboardVariable.Value : default(CharacterState);

        Event?.Invoke(newState);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        CharacterStateChannelEventHandler del = (newState) =>
        {
            BlackboardVariable<CharacterState> var0 = vars[0] as BlackboardVariable<CharacterState>;
            if(var0 != null)
                var0.Value = newState;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as CharacterStateChannelEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as CharacterStateChannelEventHandler;
    }
}

