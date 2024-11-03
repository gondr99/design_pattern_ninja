using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private int _activeCamPriority = 15, _backCamPriority = 10;

    [SerializeField] private CamSwapEventChannelSO _camSwapChannel;
    private CinemachineCamera _currentCam;

    private void Awake()
    {
        _camSwapChannel.OnValueEvent += HandleCameraChangeEvent;
        FindFirstPriorityCamera();
    }

    private void OnDestroy()
    {
        _camSwapChannel.OnValueEvent -= HandleCameraChangeEvent;
    }

    private void HandleCameraChangeEvent(CamSwapData evt)
    {
        if(_currentCam == evt.leftCam && evt.direction.x > 0)
            ChangeCamera(evt.rightCam);
        else if(_currentCam == evt.rightCam && evt.direction.x < 0)
            ChangeCamera(evt.leftCam);
    }
    
    private void FindFirstPriorityCamera()
    {
        _currentCam = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None)
            .OrderByDescending(cam => cam.Priority.Value)
            .FirstOrDefault();
        Debug.Assert(_currentCam != null, "No CinemachineCamera found!");
    }
    
    public void ChangeCamera(CinemachineCamera activeCam)
    {
        _currentCam.Priority = _backCamPriority;
        Transform followTarget = _currentCam.Follow;
        _currentCam = activeCam;
        _currentCam.Priority = _activeCamPriority;
        _currentCam.Follow = followTarget;
    }

}
