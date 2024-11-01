using UnityEngine;

public class BomberAttackState : EntityState
{
    private EnemyBomber _enemy;
    private BomberAttackCompo _atkCompo;
    
    public BomberAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _enemy = entity as EnemyBomber;
        _atkCompo = _enemy.GetCompo<BomberAttackCompo>();
    }

    public override void Enter()
    {
        base.Enter();

        FacingToPlayer();
        
        if(_atkCompo.CanAttack())
            _atkCompo.Attack();
        
    }

    public override void Update()
    {
        base.Update();
        if(_isTriggerCall)
            _enemy.ChangeState(StateName.Idle);
    }

    private void FacingToPlayer()
    {
        float xDirection = _enemy.target.transform.position.x - _enemy.transform.position.x;
        _renderer.FlipController(Mathf.Sign( xDirection) );
    }
}
