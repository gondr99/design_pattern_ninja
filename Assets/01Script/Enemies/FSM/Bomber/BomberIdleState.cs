using UnityEngine;

public class BomberIdleState : EntityState
{
    private EnemyBomber _enemy;
    private readonly float _checkTimer = 0.3f;
    private float _lastCheckTime;
    private BomberAttackCompo _atkCompo;
    
    public BomberIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _enemy = entity as EnemyBomber;
        _atkCompo = _enemy.GetCompo<BomberAttackCompo>();
    }

    public override void Enter()
    {
        base.Enter();
        _lastCheckTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (_lastCheckTime + _checkTimer < Time.time)
        {
            _lastCheckTime = Time.time;
            CheckEnemy();
        }
    }

    private void CheckEnemy()
    {
        if (_atkCompo.CanAttack() == false) return;
        
        if (_enemy.CheckPlayerInRadius() )
        {
            _enemy.ChangeState(StateName.Attack);   
        }
    }
}
