using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbCompo;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimParamSO _triggerParam;
    private float _lifeTime;
    private bool _canExplosion;
    
    public void ThrowBomb(Vector2 velocity, float lifeTime)
    {
        _canExplosion = true;
        _lifeTime = lifeTime;
        _rbCompo.AddForce(velocity, ForceMode2D.Impulse);  
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0 && _canExplosion)
        {
            _canExplosion = false;
            TriggerExplosion();
        }
    }

    private void TriggerExplosion()
    {
        _animator.SetTrigger(_triggerParam.hashValue);
    }

    private void ExplosionEnd()
    {
        Destroy(gameObject);
    }
}
