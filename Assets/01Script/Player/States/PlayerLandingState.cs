using UnityEngine;

public class PlayerLandingState : EntityState
{
    private const float LANDING_TIME = 0.3f;
    private Player _player;
    private EntityMover _mover;
    private float _landingTime;
    
    public PlayerLandingState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.CanManualMove = false;
        _landingTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (_landingTime + LANDING_TIME < Time.time)
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
