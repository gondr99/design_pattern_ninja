using UnityEngine;

public class PlayerAttackState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    
    public PlayerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(false);
        _mover.CanManualMove = false;
        _mover.AddForceToEntity(new Vector2(_renderer.FacingDirection * 1.5f, 0));
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState(StateName.Idle);
        }
    }

    public override void Exit()
    {
        _mover.CanManualMove = true;
        base.Exit();
    }
}
