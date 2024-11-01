using UnityEngine;

public class SavePoint : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimParamSO _saveTrigger;
    [SerializeField] private BoolEventChannelSO _saveChannel;
    private AnimatorCompo _animatorCompo;
    
    private void Awake()
    {
        _animatorCompo = GetComponentInChildren<AnimatorCompo>();
    }

    public void ApplyDamage(float damage, Vector2 direction, Vector2 knockBack, Entity dealer)
    {
        _animatorCompo.SetParam(_saveTrigger);
        _saveChannel.RaiseEvent(true); //파일에 세이브
    }
}
