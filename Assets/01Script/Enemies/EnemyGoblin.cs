using Unity.Behavior;
using UnityEngine;

public class EnemyGoblin : Enemy
{
    [SerializeField] private int _dropExp;
    private BlackboardVariable<bool> _isHit, _isDead;
    private BlackboardVariable<Player> _target;
    
    protected BehaviorGraphAgent _agent;
    
    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<BehaviorGraphAgent>();
    }
    private void Start()
    {
        var health = GetCompo<EntityHealth>();
        health.OnHit += HandleHit;
        health.OnDeath += HandleDeath;

        _isHit = GetVariable<bool>("IsHit");
        _isDead = GetVariable<bool>("IsDead");
        _target = GetVariable<Player>("Target");
        
        Debug.Assert(_isDead != null && _isHit != null && _target != null, "Blackboard variables are not set");
    }

    private void HandleDeath()
    {
        Debug.Log("Death");
        _isDead.Value = true;
        _expChannel.RaiseEvent(_dropExp);
    }

    private BlackboardVariable<T> GetVariable<T>(string key)
    {
        if(_agent.GetVariable(key, out BlackboardVariable<T> target))
        {
            return target;
        }
        return default;
    }

    private void HandleHit(Entity dealer)
    {
        _isHit.Value = true;
        _target.Value = dealer as Player;
    }
}
