using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _fadeChannel;
    private void Start()
    {
        _fadeChannel.RaiseEvent(true);
    }
}
