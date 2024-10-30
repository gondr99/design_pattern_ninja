using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private BoolEventChannelSO _fadeEventChannel;
        
    private readonly int _valueHash = Shader.PropertyToID("_Value");
    private void Awake()
    {
        _fadeImage.material = new Material( _fadeImage.material);
        _fadeEventChannel.OnValueEvent += HandleFadeEvent;
    }

    private void OnDestroy()
    {
        _fadeEventChannel.OnValueEvent -= HandleFadeEvent;
    }

    private void HandleFadeEvent(bool isFadeIn)
    {
        float fadeValue = isFadeIn ? 1.5f : 0f;
        float startValue = isFadeIn ? 0f : 1.5f;
            
        _fadeImage.material.SetFloat(_valueHash, startValue);

        _fadeImage.material.DOFloat(fadeValue, _valueHash, 0.8f);
    }
}
