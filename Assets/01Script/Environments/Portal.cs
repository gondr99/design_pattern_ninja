using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _fadeChannel;
    [SerializeField] private string _nextSceneName;

    private bool _isTriggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isTriggered) return;
            
        if (other.TryGetComponent(out Player player))
        {
            _isTriggered = true;
            player.GetCompo<EntityMover>().CanManualMove = false;

            _fadeChannel.RaiseEvent(false);

            DOVirtual.DelayedCall(1.2f, () => SceneManager.LoadScene(_nextSceneName));
        }
    }
}
