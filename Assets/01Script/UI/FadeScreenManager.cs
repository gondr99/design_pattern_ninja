using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private BoolEventChannelSO _fadeEventChannel, _saveOrderChannel, _loadOrderChannel;
        
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

        if (isFadeIn)
        {
            _loadOrderChannel.RaiseEvent(false); //페이드인이면 데이터 로드부터
        }
        
        var tweenCore = _fadeImage.material.DOFloat(fadeValue, _valueHash, 0.8f);

        if (isFadeIn == false)
        {
            tweenCore.OnComplete(()=> _saveOrderChannel.RaiseEvent(false));
        }
        
    }
}
