using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("FSM")] 
    [SerializeField] private EntityFSMSO _playerFSM;
    
    [Header("Data channel")] 
    public IntEventChannelSO expChannel;
    
    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
    public float jumpPower = 12f;
    public int jumpCount = 2;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    
    private int _currentJumpCount = 0;
    private EntityMover _mover;
    private PlayerAttackCompo _atkCompo;
    
    [SerializeField] private StateMachine _stateMachine;
    
    public EntityState CurrentState => _stateMachine.currentState;
    
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _stateMachine = new StateMachine(_playerFSM, this);
        
        _mover = GetCompo<EntityMover>();
        _mover.OnGroundStatusChange += HandleGroundStatusChange;
        PlayerInput.JumpEvent += HandleJumpEvent;

        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;

        _atkCompo = GetCompo<PlayerAttackCompo>();
        PlayerInput.AttackEvent += HandleAttackKeyEvent;
        PlayerInput.DashEvent += HandleDashEvent;
    }

    private void HandleExpAdd(int exp)
    {
        
    }

    private void OnDestroy()
    {
        _mover.OnGroundStatusChange -= HandleGroundStatusChange;
        PlayerInput.JumpEvent -= HandleJumpEvent;
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
        PlayerInput.AttackEvent -= HandleAttackKeyEvent;
        PlayerInput.DashEvent -= HandleDashEvent;
    }

    protected void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    public void ChangeState(StateName newState)
    {
        _stateMachine.ChageState(newState);
    }

    public EntityState GetState(StateSO state)
    {
        return _stateMachine.GetState(state.stateName);
    }
    
    private void HandleJumpEvent()
    {
        if (_mover.IsGrounded || _currentJumpCount > 0)
        {
            _currentJumpCount--;
            StateName nextState = _mover.IsGrounded ? StateName.Jump : StateName.DoubleJump;
            ChangeState(nextState);
        }
    }
    
    private void HandleGroundStatusChange(bool isGrounded)
    {
        if (isGrounded)
            _currentJumpCount = jumpCount;
    }
    
    private void HandleAnimationEnd()
    {
        _stateMachine.currentState.AnimationEndTrigger();
    }
    
    private void HandleAttackKeyEvent()
    {
        if (_atkCompo.AttemptAttack())
        {
            ChangeState(StateName.Attack);
        }
    }
    
    private void HandleDashEvent()
    {
        if (_atkCompo.AttemptDash())
        {
            ChangeState(StateName.Dash);
        }
    }

}
