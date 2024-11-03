using Unity.Cinemachine;
using UnityEngine;

public class CameraSwapTrigger : MonoBehaviour
{
    public CinemachineCamera cameraOnLeft;
    public CinemachineCamera cameraOnRight;

    [SerializeField] private CamSwapEventChannelSO _cameraSwapChannel;

    private CamSwapData _camSwapData;

    private void Awake()
    {
        _camSwapData.leftCam = cameraOnLeft;
        _camSwapData.rightCam = cameraOnRight;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 exitDirection = (other.transform.position - transform.position).normalized;
            if(cameraOnLeft != null && cameraOnRight != null)
            {
                _camSwapData.direction = exitDirection;
                _cameraSwapChannel.RaiseEvent(_camSwapData);
            }    
        }

    }

}
