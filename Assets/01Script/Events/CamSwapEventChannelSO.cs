using System;
using Unity.Cinemachine;
using UnityEngine;

public struct CamSwapData
{
    public CinemachineCamera leftCam;
    public CinemachineCamera rightCam;
    public Vector2 direction;
}

[CreateAssetMenu(fileName = "CamSwapEventChannelSO", menuName = "SO/Events/CamSwapEventChannelSO")]
public class CamSwapEventChannelSO : ScriptableObject
{
    public Action<CamSwapData> OnValueEvent;
    
    public void RaiseEvent(CamSwapData value)
    {
        OnValueEvent?.Invoke(value);    
    }
}
